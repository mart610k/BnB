use BNB;

CREATE TABLE `Room` (
  `RoomID` int NOT NULL AUTO_INCREMENT,
  `RoomAddress` varchar(100) NOT NULL,
  `RoomOwner` varchar(128) NOT NULL,
  `RoomDescription` text,
  `RoomBriefDescription` varchar(150) NOT NULL,
  PRIMARY KEY (`RoomID`),
  KEY `FK_RoomOwner_UserInformation_id` (`RoomOwner`),
  CONSTRAINT `FK_UserInformation_RoomOwner` FOREIGN KEY (`RoomOwner`) REFERENCES `userinformation` (`Email`)
);

CREATE TABLE `Status` (
  `StatusID` int NOT NULL,
  `StatusName` varchar(45) NOT NULL,
  PRIMARY KEY (`StatusID`)
);

CREATE TABLE `StatusRoom` (
  `FK_StatusID` int NOT NULL,
  `FK_RoomID` int NOT NULL,
  PRIMARY KEY (`FK_StatusID`,`FK_RoomID`),
  KEY `Room_Status_idx` (`FK_RoomID`),
  CONSTRAINT `Room_Status` FOREIGN KEY (`FK_RoomID`) REFERENCES `room` (`RoomID`),
  CONSTRAINT `Status_Room` FOREIGN KEY (`FK_StatusID`) REFERENCES `status` (`StatusID`)
);