use bnb;

CREATE TABLE `room` (
  `roomid` int NOT NULL AUTO_INCREMENT,
  `address` varchar(45) NOT NULL,
  `owner` varchar(128) NOT NULL,
  `description` text,
  `briefdescription` varchar(150) NOT NULL,
  PRIMARY KEY (`roomid`),
  FOREIGN KEY (`owner`) REFERENCES `userinformation` (`username`)
);

CREATE TABLE `status` (
  `statusid` int NOT NULL,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`statusid`)
);

CREATE TABLE `statusroom` (
  `fk_statusid` int NOT NULL,
  `fk_roomid` int NOT NULL,
  PRIMARY KEY (`fk_statusid`,`fk_roomid`),
  FOREIGN KEY (`fk_statusid`) REFERENCES `status` (`statusid`),
  FOREIGN KEY (`fk_roomid`) REFERENCES `room` (`roomid`)
);