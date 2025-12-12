# Отчет по архитектуре и реализации системы «Антиплагиат» 

## 1. Краткое описание архитектуры системы

Система построена на основе **микросервисной архитектуры** с использованием ASP.NET Core и контейнеризации Docker. Основные компоненты:

| Микросервис     | Ответственность                                                                 |
|-----------------|-------------------------------------------------------------------------------|
| **Works**       | Управление сущностями "Работа студента" (`Work`), хранение метаданных, интеграция с Analysis и FileStorage |
| **FileStorage** | Хранение файлов работ и отчётов, предоставление API для загрузки и скачивания файлов |
| **Analysis**    | Анализ присланных работ на плагиат, генерация отчётов    |
| **API Gateway** | Обеспечивает маршрутизацию запросов к соответствующим микросервисам         |

**Принципы архитектуры:**

- **Чистая архитектура (Clean Architecture):** каждый микросервис имеет слои `Domain`, `Application`, `Infrastructure` и `API`.
- **Синхронные взаимодействия:** использование `HttpClient` для синхронного запроса анализа.
- **DTO как контракты между сервисами:** обмен данными осуществляется через строго типизированные объекты `AnalyzeWorkRequest` и `AnalyzeWorkResponse`.
- **Контейнеризация:** каждый сервис упакован в Docker-контейнер и разворачивается через `docker-compose`.

---

## 2. Пользовательские сценарии микросервисов

### 2.1 Сценарий: Отправка работы студентом

**Шаги:**

1. Студент создаёт запись о работе через Works API:

```
POST /works
Content-Type: application/json
Body:
{
"StudentId": "student123",
"AssignmentId": "assignment456"
}
```

2. Works сохраняет метаданные работы в СУБД (`Work`):

- WorkId (генерируется сервером)
- StudentId
- AssignmentId
- SubmittedAt (время создания)
- FileId = null

3. API возвращает информацию о созданной работе:

```
201 Created
{
"WorkId": "guid",
"StudentId": "student123",
"AssignmentId": "assignment456",
"SubmittedAt": "2025-12-07T12:34:56Z",
"FileId": null
}
```

---

### 2.2 Сценарий: Прикрепление файла к работе

**Шаги:**

1. Студент загружает файл через отдельный эндпоинт Works API:

```
PATCH /works/{workId}/file
Content-Type: multipart/form-data
Body: file (IFormFile)
```

2. Works:

- вызывает FileStorage для загрузки файла
- получает FileId
- обновляет запись Work с новым FileId

3. API возвращает информацию о прикреплённом файле:

```
200 OK
{
"WorkId": "guid",
"FileId": "file-guid"
}
```

---

### 2.3 Сценарий: Запуск анализа работы

**Шаги:**

1. Преподаватель инициирует анализ через Works API:

```
PATCH /works/{workId}/analyze
````

2. Works:

- получает Work и FileId
- вызывает Analysis API:

  ```
  POST /analysis
  Body (AnalyzeWorkRequest):
  {
      "WorkId": "guid",
      "FileId": "file-guid",
      "StudentId": "student123",
      "AssignmentId": "assignment456",
      "SubmittedAt": "2025-12-07T12:34:56Z"
  }
  ```

3. Analysis:

- скачивает файл из FileStorage
- выполняет анализ на плагиат
- создаёт отчёт
- загружает отчёт в FileStorage и получает ReportId
- возвращает AnalyzeWorkResponse:

  ```
  {
      "ReportId": "report-guid",
      "IsPlagiarism": false,
      "Similarity": 0.0
  }
  ```

4. Works обновляет Work с ReportId и статусом анализа.

---

### 2.4 Сценарий: Получение отчёта преподавателем

**Шаги:**

1. Преподаватель запрашивает отчёт по конкретной работе:

```
GET /works/{workId}/report
```

2. Works возвращает информацию о отчёте:

```
200 OK
{
"ReportId": "report-guid",
"IsPlagiarism": false,
"Similarity": 0.0,
"FileId": "file-guid"
}
```

3. Для скачивания файла отчёта преподаватель использует FileStorage API:

```
GET /fileStorage/{fileId}
```

---

### 2.5 Технические сценарии взаимодействия сервисов

| Взаимодействие                   | Протокол  | Формат данных     | Примечания                                   |
|----------------------------------|-----------|-------------------|----------------------------------------------|
| Works → FileStorage: Upload      | HTTP POST | Multipart/FormData| Возвращает FileId                            |
| Works → Analysis: StartAnalysis  | HTTP POST | JSON (DTO)        | `AnalyzeWorkRequest` / `AnalyzeWorkResponse` |
| Analysis → FileStorage: Download | HTTP GET  | Stream            | Поток данных файла                           |
| Analysis → FileStorage: Upload   | HTTP POST | Stream            | Генерация отчёта в формате JSON              |
| Analysis → Works: Response       | HTTP GET  | JSON (DTO)        |                                              |                                          

## 3 Запуск приложения
Для запуска должен быть установлен Docker.
```bash
    docker compose up
```

После на http://localhost:8080/swagger/ можно получить доступ к Swagger для тестов API.
В правом верхнем углу экрана можно будет `Select a definition`, т.е. выбрать API какого микросервиса вам нужен.
> На `localhost:8080` уже может быть запущено другое приложение, для корректной работы убедитесь, что это не так или поменяйте порт в [docker-compose](compose.yaml) или [.env](.env)

## 4 Замечания
Так как проект учебный, он запускается как Development. Для Prod нужно (что смог вспомнить): 

1. Скрыть конфиденциальные `.env` и `appsettings.json`
2. При возникновении ошибок не возвращать весь trace с внутренними деталями системы, т.е. поменять `ErrorHandlingMiddleware`
3. Для `Word Cloud` стоит использовать только текстовые файлы; для других форматов нужно либо конвертировать, либо отключить генерацию облака слов
