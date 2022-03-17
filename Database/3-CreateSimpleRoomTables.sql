use bnb;

CREATE TABLE `room` (
  `roomid` INT PRIMARY KEY AUTO_INCREMENT,
  `address` VARCHAR(45) NOT NULL,
  `owner` VARCHAR(128) NOT NULL,
  `description` TEXT,
  `briefdescription` VARCHAR(150) NOT NULL,
  FOREIGN KEY (`owner`) REFERENCES `userinformation` (`username`)
);

CREATE TABLE `status` (
  `statusid` INT NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`statusid`)
);

CREATE TABLE `statusroom` (
  `fk_statusid` INT NOT NULL,
  `fk_roomid` INT NOT NULL,
  PRIMARY KEY (`fk_statusid`,`fk_roomid`),
  FOREIGN KEY (`fk_statusid`) REFERENCES `status` (`statusid`),
  FOREIGN KEY (`fk_roomid`) REFERENCES `room` (`roomid`)
);