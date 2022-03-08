use bnb;

 CREATE TABLE `Picture` (
  `FileName` binary(16) NOT NULL,
  `FK_RoomID` int NOT NULL,
  PRIMARY KEY (`FileName`),
  KEY `FK_Picture_Room_id` (`FK_RoomID`),
  CONSTRAINT `FK_Picture_Room` FOREIGN KEY (`FK_RoomID`) REFERENCES `Room` (`RoomID`)
);

CREATE TABLE `Facilities` (
  `FacilityID` int NOT NULL AUTO_INCREMENT,
  `FacilityName` varchar(45) NOT NULL,
  PRIMARY KEY (`FacilityID`)
);

CREATE TABLE `Facility_room` (
  `FK_RoomID` int NOT NULL,
  `FK_FacilityID` int NOT NULL,
  PRIMARY KEY (`FK_RoomID`,`FK_FacilityID`),
  KEY `Facility_Room_id` (`FK_FacilityID`),
  CONSTRAINT `Facility_Room` FOREIGN KEY (`FK_FacilityID`) REFERENCES `Facilities` (`FacilityID`),
  CONSTRAINT `Room_Facility` FOREIGN KEY (`FK_RoomID`) REFERENCES `Room` (`RoomID`)
);