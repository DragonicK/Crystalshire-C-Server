/*
 Navicat Premium Data Transfer

 Source Server         : Connection
 Source Server Type    : MySQL
 Source Server Version : 80012
 Source Host           : localhost:3306
 Source Schema         : yugioh

 Target Server Type    : MySQL
 Target Server Version : 80012
 File Encoding         : 65001

 Date: 02/02/2019 18:23:01
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for accountban
-- ----------------------------
DROP TABLE IF EXISTS `accountban`;
CREATE TABLE `accountban`  (
  `BanId` int(11) NOT NULL AUTO_INCREMENT,
  `AccountId` int(11) NOT NULL,
  `Reason` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `Expired` tinyint(4) NOT NULL DEFAULT 0,
  `ExpireDate` datetime(0) NULL DEFAULT NULL,
  `Permanent` tinyint(4) NOT NULL DEFAULT 0,
  PRIMARY KEY (`BanId`) USING BTREE,
  INDEX `BanIndex1`(`AccountId`) USING BTREE,
  INDEX `BanIndex2`(`BanId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of accountban
-- ----------------------------
INSERT INTO `accountban` VALUES (1, 1, 'cu cabeludo', 1, '2019-04-25 18:53:11', 0);
INSERT INTO `accountban` VALUES (2, 1, 'xana pelada', 1, '2019-03-14 18:54:21', 0);

-- ----------------------------
-- Table structure for accountdata
-- ----------------------------
DROP TABLE IF EXISTS `accountdata`;
CREATE TABLE `accountdata`  (
  `AccountId` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(25) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `Passphrase` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `Email` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `Cash` int(11) NOT NULL DEFAULT 0,
  `Activated` tinyint(4) NOT NULL DEFAULT 0,
  `AccountKey` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `AccountLevelCode` tinyint(4) NOT NULL DEFAULT 1,
  `LoggedIn` tinyint(4) NOT NULL DEFAULT 0,
  `LastLoginDate` datetime(0) NULL DEFAULT NULL,
  `LastLogoutDate` datetime(0) NULL DEFAULT NULL,
  `ServiceId` tinyint(4) NOT NULL DEFAULT 0,
  `LastLoginIp` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `CurrentSessionId` varchar(17) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `CurrentIp` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `NewbieRewardDate` datetime(0) NULL DEFAULT NULL,
  `NewbieRewardFlag` tinyint(4) NOT NULL DEFAULT 0,
  `ReturnRewardFlag` tinyint(4) NOT NULL DEFAULT 0,
  `RewardPoints` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`AccountId`) USING BTREE,
  INDEX `AccountIndex1`(`Username`) USING BTREE,
  INDEX `AccountIndex2`(`AccountId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of accountdata
-- ----------------------------
INSERT INTO `accountdata` VALUES (1, 'akaruz', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', '', 2439, 1, '', 4, 0, '2019-02-02 18:15:10', NULL, 5, '', '', '', NULL, 0, 0, 0);
INSERT INTO `accountdata` VALUES (2, 'dragonick', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', '', 0, 1, '', 4, 0, '2019-02-02 18:00:42', NULL, 0, '', '', '', NULL, 0, 0, 0);

-- ----------------------------
-- Table structure for characterdata
-- ----------------------------
DROP TABLE IF EXISTS `characterdata`;
CREATE TABLE `characterdata`  (
  `CharacterId` int(11) NOT NULL AUTO_INCREMENT,
  `AccountId` int(11) NOT NULL DEFAULT 0,
  `CharacterIndex` tinyint(4) NOT NULL DEFAULT 0,
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `Classe` int(11) NOT NULL DEFAULT 0,
  `Sex` tinyint(4) NOT NULL DEFAULT 0,
  `Sprite` int(11) NOT NULL DEFAULT 0,
  `Level` int(11) NOT NULL DEFAULT 0,
  `Experience` int(11) NOT NULL DEFAULT 0,
  `Inventory` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Map` int(11) NOT NULL DEFAULT 0,
  `X` int(11) NOT NULL DEFAULT 0,
  `Y` int(11) NOT NULL DEFAULT 0,
  `Dir` int(11) NOT NULL DEFAULT 0,
  `LastLoginDate` datetime(0) NULL DEFAULT NULL,
  `LastLogoutDate` datetime(0) NULL DEFAULT NULL,
  `PendingExclusion` tinyint(4) NOT NULL DEFAULT 0,
  `ExclusionDate` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`CharacterId`) USING BTREE,
  INDEX `CharacterIndex1`(`CharacterId`) USING BTREE,
  INDEX `CharacterIndex2`(`AccountId`, `CharacterIndex`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 20 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Procedure structure for DeleteCharacter
-- ----------------------------
DROP PROCEDURE IF EXISTS `DeleteCharacter`;
delimiter ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteCharacter`(UserId INT(11))
BEGIN
	DELETE FROM CharacterData WHERE CharacterID = UserId;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for TruncateCharacterData
-- ----------------------------
DROP PROCEDURE IF EXISTS `TruncateCharacterData`;
delimiter ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `TruncateCharacterData`()
BEGIN
  TRUNCATE characterdata;
END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
