BEGIN TRANSACTION;
DROP TABLE IF EXISTS "TConfig";
CREATE TABLE IF NOT EXISTS "TConfig" (
	"FID"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"FParentID"	INTEGER NULL,
	"FSeqNo"	INTEGER NULL,
	"FKey"	TEXT NOT NULL,
	"FValue"	TEXT NOT NULL,
	"FValueB"	TEXT NULL,
	"FReadonly"	INTEGER NULL,
	"FNote"	TEXT NULL,
	"FCreateTime"	DATETIME NOT NULL DEFAULT (datetime('now', 'localtime')),
	"FUpdateTime"	DATETIME NOT NULL DEFAULT (datetime('now', 'localtime'))
);
DROP INDEX IF EXISTS "IParentKey";
CREATE UNIQUE INDEX IF NOT EXISTS "IParentKey" ON "TConfig" (
	"FParentID",
	"FKey"
);

INSERT into TConfig(FID, FParentID, FSeqNo, FKey, FValue, FValueB, FReadonly, FNote, FCreateTime, FUpdateTime) VALUES (0, NULL, NULL, 'KeyList', '代碼清單', NULL, 1, 'FKey for 程式控制.' || CHAR(13) || CHAR(10) || 'FValue for 主要語言.' || CHAR(13) || CHAR(10) || 'FValueB for 次要語言或匯入語言.', (datetime('now', 'localtime')), (datetime('now', 'localtime')));

update sqlite_sequence set seq = 999 where name = 'TConfig';
INSERT into TConfig(FID, FParentID, FSeqNo, FKey, FValue, FValueB, FReadonly, FNote, FCreateTime, FUpdateTime) VALUES (NULL, 0, NULL, 'Gender', '性別', NULL, NULL, 'Male 中文為男,' || CHAR(13) || CHAR(10) || 'Female 中文是女.', (datetime('now', 'localtime')), (datetime('now', 'localtime')));
INSERT into TConfig(FID, FParentID, FSeqNo, FKey, FValue, FValueB, FReadonly, FNote, FCreateTime, FUpdateTime) VALUES (NULL, 1000, 10, 'M', '男', 'Male', NULL, NULL, (datetime('now', 'localtime')), (datetime('now', 'localtime')));
INSERT into TConfig(FID, FParentID, FSeqNo, FKey, FValue, FValueB, FReadonly, FNote, FCreateTime, FUpdateTime) VALUES (NULL, 1000, 20, 'F', '女', 'Female', NULL, NULL, (datetime('now', 'localtime')), (datetime('now', 'localtime')));
INSERT into TConfig(FID, FParentID, FSeqNo, FKey, FValue, FValueB, FReadonly, FNote, FCreateTime, FUpdateTime) VALUES (NULL, 0, NULL, 'Marriage', '婚姻', NULL, NULL, '婚姻狀態除了單身和已婚以外,' || CHAR(13) || CHAR(10) || '還可以細分為單身未婚、單身已婚.', (datetime('now', 'localtime')), (datetime('now', 'localtime')));
INSERT into TConfig(FID, FParentID, FSeqNo, FKey, FValue, FValueB, FReadonly, FNote, FCreateTime, FUpdateTime) VALUES (NULL, 1003, 10, 'Single', '單身', NULL, NULL, NULL, (datetime('now', 'localtime')), (datetime('now', 'localtime')));
INSERT into TConfig(FID, FParentID, FSeqNo, FKey, FValue, FValueB, FReadonly, FNote, FCreateTime, FUpdateTime) VALUES (NULL, 1003, 20, 'Married', '已婚', NULL, NULL, NULL, (datetime('now', 'localtime')), (datetime('now', 'localtime')));

COMMIT;