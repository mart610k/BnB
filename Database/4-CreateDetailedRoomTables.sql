USE bnb;

CREATE TABLE `picture` (
  `filename` binary(16) NOT NULL,
  `fk_roomid` int NOT NULL,
  PRIMARY KEY (`filename`),
  FOREIGN KEY (`fk_roomid`) REFERENCES `room` (`roomid`)
);

CREATE TABLE `facility` (
  `facilityid` int NOT NULL AUTO_INCREMENT,
  `facilityname` varchar(45) NOT NULL,
  PRIMARY KEY (`facilityid`)
);

CREATE TABLE `facility_room` (
  `fk_roomid` int NOT NULL,
  `fk_facilityid` int NOT NULL,
  PRIMARY KEY (`fk_roomid`,`fk_facilityid`),
  FOREIGN KEY (`fk_roomid`) REFERENCES `room` (`roomid`),
  FOREIGN KEY (`fk_facilityid`) REFERENCES `facility` (`facilityid`)
);
