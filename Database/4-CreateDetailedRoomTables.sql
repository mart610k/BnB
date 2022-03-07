use bnb;

 CREATE TABLE `picture` (
  `FileName` binary(16) NOT NULL,
  `FK_RoomID` int NOT NULL,
  PRIMARY KEY (`FileName`),
  KEY `FK_Picture_Room_idx` (`FK_RoomID`),
  CONSTRAINT `FK_Picture_Room` FOREIGN KEY (`FK_RoomID`) REFERENCES `room` (`RoomID`)
);

CREATE TABLE `facilities` (
  `FacilityID` int NOT NULL AUTO_INCREMENT,
  `FacilityName` varchar(45) NOT NULL,
  PRIMARY KEY (`FacilityID`)
);

CREATE TABLE `facility_room` (
  `FK_RoomID` int NOT NULL,
  `FK_FacilityID` int NOT NULL,
  PRIMARY KEY (`FK_RoomID`,`FK_FacilityID`),
  KEY `Facility_Room_idx` (`FK_FacilityID`),
  CONSTRAINT `Facility_Room` FOREIGN KEY (`FK_FacilityID`) REFERENCES `facilities` (`FacilityID`),
  CONSTRAINT `Room_Facility` FOREIGN KEY (`FK_RoomID`) REFERENCES `room` (`RoomID`)
);