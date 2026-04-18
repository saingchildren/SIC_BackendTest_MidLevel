# DB部分修正

* 因應採用api方式，所以MyOffice_ExcuteionLog的欄位【DeLog_StoredPrograms】修正為【DeLog_Api】。
* 貴司所提供的【usp_AddLog 記錄執行錯誤.sql】內的參數宣告【@_InBox_ExProgram】長度為40，修正為與schema同步的長度120。
