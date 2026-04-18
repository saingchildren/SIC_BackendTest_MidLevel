## 技術採用

- **框架:** .NET Core 8 Web API
- **資料庫:** SQL Server 2019+
- **ORM 框架:** Entity Framework Core 8 + Dapper
- **API 文件:** Swagger (Swashbuckle)
- **版本控制:** Git & GitHub

## 專案架構

本專案採用分層式架構，清楚區分Controller、Service、Model，讓每個程式都達到職責分離，而記錄Api 請求Log採用Action Filter呼叫SP的方式做紀錄。

## 執行步驟

### 資料庫還原

取得根目錄下的資料庫備份檔 `Mercuryfire_Test.bak`。
開啟 SQL Server Management Studio (SSMS)，將此備份檔還原至你的本機 SQL Server。

### 設定連線字串

開啟專案目錄下的 `appsettings.json` (或 `appsettings.Development.json`)。
找到 `ConnectionStrings` 區段，將 `ConnectionString` 修改為你本機 SQL Server 的連線資訊：

```json
"ConnectionStrings": {
  "ConnectionString": "Server=YOUR_SERVER_NAME;Database=Mercuryfire_Test;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
}
```

## DB部分修正

- 因應採用api方式，所以MyOffice_ExcuteionLog的欄位【DeLog_StoredPrograms】修正為【DeLog_Api】。
- 貴司所提供的【usp_AddLog 記錄執行錯誤.sql】內的參數宣告【@\_InBox_ExProgram】長度為40，修正為與schema同步的長度120。
