USE bnb;

CREATE TABLE `picture` (
  `filename` BINARY(16) PRIMARY KEY NOT NULL,
  `fk_roomid` INT NOT NULL,
  FOREIGN KEY (`fk_roomid`) REFERENCES `room` (`roomid`)
);

CREATE TABLE `facility` (
  `facilityid` INT PRIMARY KEY  NOT NULL AUTO_INCREMENT,
  `facilityname` VARCHAR(45) NOT NULL
);

CREATE TABLE `facility_room` (
  `fk_roomid` INT NOT NULL,
  `fk_facilityid` INT NOT NULL,
  PRIMARY KEY (`fk_roomid`,`fk_facilityid`),
  FOREIGN KEY (`fk_roomid`) REFERENCES `room` (`roomid`),
  FOREIGN KEY (`fk_facilityid`) REFERENCES `facility` (`facilityid`)
);
