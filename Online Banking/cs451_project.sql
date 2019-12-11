-- MySQL dump 10.13  Distrib 8.0.17, for Win64 (x86_64)
--
-- Host: KC-ISIA-MySQL1D    Database: cs451r_fs2019_group6
-- ------------------------------------------------------
-- Server version	5.7.26-enterprise-commercial-advanced-log
/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
--
-- Table structure for table `account_types`
--
DROP TABLE IF EXISTS account_types;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE account_types (
  Account_Type_ID varchar(3) NOT NULL,
  Account_Type_Name varchar(60) NOT NULL,
  PRIMARY KEY (Account_Type_ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
--
-- Dumping data for table `account_types`
--
LOCK TABLES account_types WRITE;
INSERT INTO account_types VALUES ('CD','certificate of deposit'),('CHK','checking account'),('SAV','savings account');
UNLOCK TABLES;
--
-- Table structure for table `accounts`
--
DROP TABLE IF EXISTS accounts;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE accounts (
  Account_ID int(10) unsigned NOT NULL AUTO_INCREMENT,
  AcctOpen_Date date NOT NULL,
  AcctClose_Date date DEFAULT NULL,
  AcctStatus enum('ACTIVE','CLOSED','FROZEN') DEFAULT NULL,
  Account_Type_ID varchar(3) DEFAULT NULL,
  PRIMARY KEY (Account_ID),
  KEY fk_accounts_account_types (Account_Type_ID),
  CONSTRAINT fk_accounts_account_types FOREIGN KEY (Account_Type_ID) REFERENCES account_types (Account_Type_ID)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
--
-- Dumping data for table `accounts`
--
LOCK TABLES accounts WRITE;
INSERT INTO accounts VALUES (5,'2015-12-05',NULL,'ACTIVE','CHK'),(6,'2015-12-05',NULL,'ACTIVE','SAV');
UNLOCK TABLES;
--
-- Table structure for table `alerts`
--
DROP TABLE IF EXISTS alerts;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE alerts (
  Transaction_ID int(10) unsigned NOT NULL,
  Account_ID int(10) unsigned NOT NULL,
  Customer_ID int(10) unsigned DEFAULT NULL,
  AlertReason varchar(30) NOT NULL,
  Deleted tinyint(1) unsigned DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
--
-- Dumping data for table `alerts`
--
LOCK TABLES alerts WRITE;
INSERT INTO alerts VALUES (42,5,2,'Out of State',0),(41,5,2,'Out of State',0),(13,5,2,'Withdrawal Over Amount',0);
UNLOCK TABLES;
--
-- Table structure for table `customer_accounts`
--
DROP TABLE IF EXISTS customer_accounts;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE customer_accounts (
  Customer_ID int(11) NOT NULL,
  Account_ID int(11) NOT NULL,
  KEY CustomerID_idx (Customer_ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
--
-- Dumping data for table `customer_accounts`
--
LOCK TABLES customer_accounts WRITE;
INSERT INTO customer_accounts VALUES (2,5),(2,6);
UNLOCK TABLES;
--
-- Table structure for table `customers`
--
DROP TABLE IF EXISTS customers;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE customers (
  Customer_ID int(10) unsigned NOT NULL AUTO_INCREMENT,
  CustStreet varchar(30) DEFAULT NULL,
  CustCity varchar(20) DEFAULT NULL,
  State_ID varchar(2) DEFAULT NULL,
  CustZip_code varchar(10) DEFAULT NULL,
  CustFirst_Name varchar(20) NOT NULL,
  CustLast_Name varchar(20) NOT NULL,
  CustBirth_Date date NOT NULL,
  CustEmail varchar(30) DEFAULT NULL,
  CustPhone varchar(15) NOT NULL,
  CustUserName varchar(20) NOT NULL,
  CustPassword varchar(20) NOT NULL,
  PRIMARY KEY (Customer_ID)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
--
-- Dumping data for table `customers`
--
LOCK TABLES customers WRITE;
INSERT INTO customers VALUES (2,'11 Rise St','Kansas City','MO','64123','John','Smith','1970-06-22','john.smith@gmail.com','816-123-4567','jsmith','a1');
UNLOCK TABLES;
--
-- Table structure for table `previousreports`
--
DROP TABLE IF EXISTS previousreports;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE previousreports (
  StartDate varchar(10) NOT NULL,
  EndDate varchar(10) NOT NULL,
  AlertsInTimePeriod int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
--
-- Dumping data for table `previousreports`
--
LOCK TABLES previousreports WRITE;
INSERT INTO previousreports VALUES ('07/01/19','07/31/19',2),('06/01/19','06/30/19',0),('05/01/19','05/31/19',1);
UNLOCK TABLES;
--
-- Table structure for table `reports`
--
DROP TABLE IF EXISTS reports;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE reports (
  Rules varchar(30) NOT NULL,
  TimesRecent int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
--
-- Dumping data for table `reports`
--
LOCK TABLES reports WRITE;
INSERT INTO reports VALUES ('Out of State',0),('Withdrawal Over Amount',0);
UNLOCK TABLES;
--
-- Table structure for table `rules`
--
DROP TABLE IF EXISTS rules;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE rules (
  Rule_ID int(11) NOT NULL AUTO_INCREMENT,
  Customer_ID int(11) NOT NULL,
  Account_ID int(11) NOT NULL,
  OutStateTrans tinyint(1) unsigned zerofill DEFAULT NULL,
  RangeTrans tinyint(1) unsigned zerofill DEFAULT NULL,
  StartTrans varchar(10) DEFAULT NULL,
  EndTrans varchar(10) DEFAULT NULL,
  CatTrans tinyint(1) unsigned zerofill DEFAULT NULL,
  Catagory varchar(45) DEFAULT NULL,
  GreatTrans tinyint(1) unsigned zerofill DEFAULT NULL,
  GreatTransAmt double unsigned zerofill DEFAULT NULL,
  GreatDepo tinyint(1) unsigned zerofill DEFAULT NULL,
  GreatDepoAmt double DEFAULT NULL,
  GreatWithdraw tinyint(1) unsigned zerofill DEFAULT NULL,
  GreatWithdrawAmt double unsigned zerofill DEFAULT NULL,
  GreatBal tinyint(1) unsigned zerofill DEFAULT NULL,
  GreatBalAmt double unsigned zerofill DEFAULT NULL,
  LessBal tinyint(1) unsigned zerofill DEFAULT NULL,
  LessBalAmt double unsigned zerofill DEFAULT NULL,
  PRIMARY KEY (Rule_ID)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
--
-- Dumping data for table `rules`
--
LOCK TABLES rules WRITE;
INSERT INTO rules VALUES (1,2,5,1,0,'','',0,'',0,0000000000000000000000,0,0,1,0000000000000000000500,0,0000000000000000000000,0,0000000000000000000000);
UNLOCK TABLES;
--
-- Table structure for table `transactions`
--
DROP TABLE IF EXISTS transactions;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE transactions (
  Transaction_id int(10) unsigned NOT NULL AUTO_INCREMENT,
  Account_ID int(10) unsigned NOT NULL,
  TrnsDate varchar(10) NOT NULL,
  TrnsType enum('DR','CR') DEFAULT NULL,
  TrnsAmount decimal(10,2) NOT NULL,
  TrnsName varchar(50) NOT NULL,
  TrnsLocation varchar(2) DEFAULT NULL,
  TrnsBalance decimal(10,2) NOT NULL,
  PRIMARY KEY (Transaction_id),
  KEY fk_transactions_accounts (Account_ID),
  CONSTRAINT fk_transactions_accounts FOREIGN KEY (Account_ID) REFERENCES `accounts` (Account_ID) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=75 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
--
-- Dumping data for table `transactions`
--
LOCK TABLES transactions WRITE;
INSERT INTO transactions VALUES (1,5,'05/01/19','CR',0.00,'Initial','MO',5000.00),(2,5,'05/02/19','DR',2.00,'Starbucks','MO',4998.00),(3,5,'05/04/19','CR',800.00,'Payroll','MO',5798.00),(4,5,'05/07/19','DR',8.00,'Chipotle','MO',5790.00),(5,5,'05/09/19','DR',10.00,'ATM','MO',5780.00),(6,5,'05/10/19','DR',32.00,'Hoolihans','MO',5748.00),(7,5,'05/10/19','DR',100.00,'KCPL','MO',5648.00),(8,5,'05/10/19','DR',190.00,'Google Fiber','MO',5458.00),(9,5,'05/15/19','DR',9.99,'Netflix','MO',5448.01),(10,5,'05/15/19','DR',350.00,'Rent','MO',5098.01),(11,5,'05/19/19','DR',2.00,'Starbucks','MO',5096.01),(12,5,'05/19/19','CR',750.00,'Payroll','MO',5846.01),(13,5,'05/19/19','DR',620.00,'Commerce Bank Credit Card payment','MO',5226.01),(14,5,'05/19/19','DR',150.00,'McFaddens','MO',5076.01),(15,5,'05/22/19','DR',100.00,'Price Chopper','MO',4976.01),(16,5,'05/23/19','CR',50.00,'Check from friend','MO',5026.01),(17,5,'06/02/19','CR',800.00,'Payroll','MO',5826.01),(18,5,'06/02/19','DR',9.00,'Hyvee','MO',5817.01),(19,5,'06/06/19','DR',14.00,'McFaddens','MO',5803.01),(20,5,'06/13/19','DR',32.00,'Target','MO',5771.01),(21,5,'06/13/19','DR',100.00,'KCPL','MO',5671.01),(22,5,'06/19/19','CR',750.00,'Payroll','MO',6421.01),(23,5,'06/19/19','DR',190.00,'Google Fiber','MO',6231.01),(24,5,'06/19/19','DR',9.99,'Netflix','MO',6221.02),(25,5,'06/19/19','DR',350.00,'Rent','MO',5871.02),(26,5,'06/22/19','DR',6.50,'Neo\'s','MO',5864.52),(27,5,'06/22/19','DR',230.00,'Commerce Bank Credit Card payment','MO',5634.52),(28,5,'06/22/19','DR',100.00,'Best Buy','MO',5534.52),(29,5,'06/22/19','DR',300.00,'Pottery Barn','MO',5234.52),(30,5,'06/26/19','DR',23.00,'The Loft','MO',5211.52),(31,5,'06/26/19','DR',45.00,'Dave and Busters','MO',5166.52),(32,5,'06/26/19','DR',35.00,'Bowling','MO',5131.52),(33,5,'06/30/19','CR',800.00,'Payroll','MO',5931.52),(34,5,'06/31/19','DR',210.00,'McFaddens','MO',5721.52),(35,5,'06/31/19','DR',18.00,'Taco Bell','MO',5703.52),(36,5,'06/31/19','DR',45.00,'QuikTrip','MO',5658.52),(37,5,'07/02/19','DR',130.00,'KCPL','MO',5528.52),(38,5,'07/02/19','DR',185.00,'Google Fiber','MO',5343.52),(39,5,'07/04/19','DR',7.99,'Netflix','MO',5335.53),(40,5,'07/04/19','DR',56.00,'Price Chopper','MO',5279.53),(41,5,'07/06/19','DR',43.00,'Price Chopper','NE',5236.53),(42,5,'07/07/19','DR',98.00,'Target','NE',5138.53),(43,5,'07/09/19','DR',13.00,'Jose Peppers','MO',5125.53),(44,5,'07/10/19','DR',9.00,'Starbucks','MO',5116.53),(45,5,'07/12/19','DR',350.00,'Rent','MO',4766.53),(46,5,'07/12/19','DR',3.50,'Redbox','MO',4763.03),(47,5,'07/12/19','DR',301.00,'Bank of America Credit Card payment','MO',4462.03),(48,5,'07/13/19','CR',730.00,'Payroll','MO',5192.03),(49,5,'07/14/19','DR',232.68,'Target','MO',4959.35),(50,5,'07/14/19','DR',18.50,'Best Buy','MO',4940.85),(51,5,'07/16/19','DR',120.00,'Nationwide','MO',4820.85),(52,5,'07/19/19','DR',50.00,'KC Police - Speeding Ticket','MO',4770.85),(53,5,'07/19/19','DR',50.00,'Uber','MO',4720.85),(54,5,'07/19/19','DR',9.20,'Manny\'s','MO',4711.65),(55,5,'07/19/19','DR',24.75,'Toys R Us','MO',4686.90),(56,5,'07/19/19','DR',3.50,'Scooters','MO',4683.40),(57,5,'07/20/19','DR',36.00,'QuikTrip','MO',4647.40),(58,5,'07/20/19','DR',32.00,'Price Chopper','MO',4615.40),(59,5,'07/21/19','DR',48.12,'Home Depot','MO',4567.28),(60,5,'07/22/19','DR',4.20,'Burger King','MO',4563.08),(61,5,'07/22/19','DR',45.00,'Jiffy Lube','MO',4518.08),(62,5,'07/22/19','DR',25.00,'Doctor visit','MO',4493.08),(63,5,'07/23/19','DR',36.00,'CVS','MO',4457.08),(64,5,'07/23/19','DR',29.00,'Price Chopper','MO',4428.08),(65,5,'07/23/19','DR',200.00,'Transfer to Savings','MO',4228.08),(66,5,'07/23/19','CR',150.00,'Christmas Check from Grandma','MO',4378.08),(67,5,'07/23/19','DR',250.00,'Student loans','MO',4128.08),(68,5,'07/23/19','DR',75.00,'Ford Service','MO',4053.08),(69,5,'07/27/19','DR',36.00,'Hallmark','MO',4017.08),(70,5,'07/27/19','DR',22.00,'CVS','MO',3995.08),(71,5,'07/27/19','CR',810.00,'Payroll','MO',4805.08),(72,5,'07/30/19','DR',180.00,'Pottery Barn','MO',4625.08),(73,5,'07/30/19','DR',46.00,'Cheesecake Factory','MO',4579.08),(74,5,'07/30/19','DR',8.00,'Starbucks','MO',4571.08);
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`CS451R_FS19G6usr`@`%`*/ /*!50003 TRIGGER `cs451r_fs2019_group6`.`transactions_AFTER_INSERT` AFTER INSERT ON `transactions` FOR EACH ROW
BEGIN
	CALL transAlert(NEW.Account_ID, NEW.Transaction_id, NEW.TrnsLocation, NEW.TrnsDate, NEW.TrnsName, NEW.TrnsAmount, NEW.TrnsType, NEW.TrnsBalance);
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
--
-- Dumping events for database 'cs451r_fs2019_group6'
--
--
-- Dumping routines for database 'cs451r_fs2019_group6'
--
/*!50003 DROP PROCEDURE IF EXISTS transAlert */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=CS451R_FS19G6usr@`%` PROCEDURE transAlert(accountID int(10), transID int(10), transLoc varchar(2), tdate varchar(10), tCat varchar(45), tAmt decimal(10,2), tType enum('DR','CR'), tBal decimal(10,2))
BEGIN
	DECLARE finished INTEGER DEFAULT 0;
    DECLARE Customer int(10);
	DECLARE curCustomers CURSOR FOR SELECT Customer_ID FROM customer_accounts WHERE Account_ID = accountID;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET finished = 1;
    
    OPEN curCustomers;
    getCustomers: LOOP
		FETCH curCustomers INTO Customer;
        IF finished = 1 THEN
			LEAVE getCustomers;
		END IF;
        
        SELECT OutStateTrans, RangeTrans, StartTrans, EndTrans, CatTrans, Catagory, GreatTrans, GreatTransAmt,
			GreatDepo, GreatDepoAmt, GreatWithdraw, GreatWithdrawAmt, GreatBal, GreatBalAmt, LessBal, LessBalAmt
			INTO @OutState, @RangeChk, @StartDte, @EndDte, @CatChk, @Cat, @GrtTrnChk, @GrtTrnAmt, @GrtDepoChk,
				@GrtDepoAmt, @GrtDRChk, @GrtDRAmt, @GrtBalChk, @GrtBalAmt, @LsBalChk, @LsBalAmt
				FROM rules WHERE Customer_ID = Customer AND Account_ID = accountID;
                
        IF (@OutState AND (SELECT State_ID FROM customers WHERE Customer_ID = Customer) <> transLoc) THEN
			INSERT INTO `cs451_project`.`alerts` VALUES (transID, accountID, Customer, 'Out of state', 0);
		ELSEIF (@RangeChk AND (STR_TO_DATE(@StartDte,'%Y-%m-%d') <= STR_TO_DATE(tdate,'%m/%d/%Y')
				AND STR_TO_DATE(@EndDte,'%Y-%m-%d') >= STR_TO_DATE(tdate,'%m/%d/%Y'))) THEN
			INSERT INTO `cs451_project`.`alerts` VALUES (transID, accountID, Customer, 'Flagged date range', 0);
		ELSEIF (@CatChk AND @Cat = tCat) THEN
			INSERT INTO `cs451_project`.`alerts` VALUES (transID, accountID, Customer, 'Flagged catagory', 0);
		ELSEIF (@GrtTrnChk AND tAmt > @GrtTrnAmt) THEN
			INSERT INTO `cs451_project`.`alerts` VALUES (transID, accountID, Customer, 'Excessive Transaction', 0);
        ELSEIF (@GrtDepoChk AND tType = 'CR' AND tAmt > @GrtDepoAmt) THEN
			INSERT INTO `cs451_project`.`alerts` VALUES (transID, accountID, Customer, 'Excessive Deposit', 0);
		ELSEIF (@GrtDRChk AND tType = 'DR' AND tAmt > @GrtDRAmt) THEN
			INSERT INTO `cs451_project`.`alerts` VALUES (transID, accountID, Customer, 'Excessive Withdraw', 0);
		ELSEIF (@GrtBalChk AND tBal > @GrtBalAmt) THEN
			INSERT INTO `cs451_project`.`alerts` VALUES (transID, accountID, Customer, 'High Balance', 0);
		ELSEIF (@LsBalChk AND tBal < @LsBalAmt) THEN
			INSERT INTO `cs451_project`.`alerts` VALUES (transID, accountID, Customer, 'Low Balance', 0);
        END IF;
        
    END LOOP getCustomers;
    CLOSE curCustomers;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;