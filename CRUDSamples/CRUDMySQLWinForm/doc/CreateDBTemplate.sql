-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: dbtemplate
-- ------------------------------------------------------
-- Server version	5.7.21-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tconfig`
--

DROP TABLE IF EXISTS `tconfig`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tconfig` (
  `FID` int(11) NOT NULL,
  `FParentID` int(11) DEFAULT NULL,
  `FSeqNo` int(11) DEFAULT NULL,
  `FKey` varchar(256) NOT NULL,
  `FValue` varchar(2048) DEFAULT NULL,
  `FValueB` varchar(2048) DEFAULT NULL,
  `FReadonly` int(11) DEFAULT NULL,
  `FNote` varchar(2048) DEFAULT NULL,
  `FCreateTime` datetime NOT NULL,
  `FUpdateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`FID`),
  UNIQUE KEY `IParentKey` (`FParentID`,`FKey`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tconfig`
--

LOCK TABLES `tconfig` WRITE;
/*!40000 ALTER TABLE `tconfig` DISABLE KEYS */;
INSERT INTO `tconfig` VALUES (0,NULL,NULL,'KeyList','代碼清單',NULL,1,'FKey for 程式控制.\r\nFValue for 主要語言.\r\nFValueB for 次要語言或匯入語言.','2020-01-17 13:33:03','2020-01-17 13:33:03'),(1000,0,NULL,'Gender','性別',NULL,NULL,'Male 中文為男,\r\nFemale 中文是女.','2020-01-17 13:33:03','2020-01-17 13:33:03'),(1001,1000,10,'M','男','Male',NULL,NULL,'2020-01-17 13:33:03','2020-01-17 13:33:03'),(1002,1000,20,'F','女','Female',NULL,NULL,'2020-01-17 13:33:03','2020-01-17 13:33:03'),(1003,0,NULL,'Marriage','婚姻',NULL,NULL,'婚姻狀態除了單身和已婚以外,\r\n還可以細分為單身未婚、單身已婚.','2020-01-17 13:33:03','2020-01-17 13:33:03'),(1004,1003,10,'Single','單身',NULL,NULL,NULL,'2020-01-17 13:33:03','2020-01-17 13:33:03'),(1005,1003,20,'Married','已婚',NULL,NULL,NULL,'2020-01-17 13:33:03','2020-01-17 13:33:03');
/*!40000 ALTER TABLE `tconfig` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-01-20 13:55:00
