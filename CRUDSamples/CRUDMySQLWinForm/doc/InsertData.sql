-- InsertData.sql
-- 20200117, Honda, Create.

USE `dbtemplate`;

SET SQL_SAFE_UPDATES=0;
delete from `tconfig` where 1=1;
SET SQL_SAFE_UPDATES=1;

ALTER TABLE `tconfig` AUTO_INCREMENT = 0;
SET SESSION sql_mode='NO_AUTO_VALUE_ON_ZERO';

INSERT INTO `tconfig` VALUES (0,NULL,NULL,'KeyList','代碼清單',NULL,1,CONCAT('FKey for 程式控制.', CHAR(13, 10 USING utf8),'FValue for 主要語言.', CHAR(13, 10 USING utf8),'FValueB for 次要語言或匯入語言.'), now(), now());
INSERT INTO `tconfig` VALUES (1000, 0, NULL, 'Gender', '性別', NULL, NULL, CONCAT('Male 中文為男,', CHAR(13, 10 USING utf8),'Female 中文是女.'), now(), now());
INSERT INTO `tconfig` VALUES (1001, 1000, 10, 'M', '男', 'Male', NULL, NULL, now(), now());
INSERT INTO `tconfig` VALUES (1002, 1000, 20, 'F', '女', 'Female', NULL, NULL, now(), now());
INSERT INTO `tconfig` VALUES (1003, 0, NULL, 'Marriage', '婚姻', NULL, NULL, CONCAT('婚姻狀態除了單身和已婚以外,', CHAR(13, 10 USING utf8),'還可以細分為單身未婚、單身已婚.'), now(), now());
INSERT INTO `tconfig` VALUES (1004, 1003, 10, 'Single', '單身', NULL, NULL, NULL, now(), now());
INSERT INTO `tconfig` VALUES (1005, 1003, 20, 'Married', '已婚', NULL, NULL, NULL, now(), now());

ALTER TABLE `tconfig` AUTO_INCREMENT = 1006;
SET SESSION sql_mode=(SELECT REPLACE(@@sql_mode,'NO_AUTO_VALUE_ON_ZERO',''));

