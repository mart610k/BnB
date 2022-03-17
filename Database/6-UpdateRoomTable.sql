USE bnb;


ALTER TABLE `room` ADD `price` INT DEFAULT 0 NOT NULL;

DROP TABLE `statusroom`;

DROP TABLE `status`;

CREATE TABLE `rent`(
    `fk_roomid` INT NOT NULL, 
    `from` DATE NOT NULL, 
    `to` DATE NOT NULL, 
    `fk_userid` VARCHAR(128) NOT NULL, 
    `accepted` BOOLEAN NOT NULL,
    FOREIGN KEY(`fk_roomid`) REFERENCES `room`(`roomid`), 
    FOREIGN KEY(`fk_userid`) REFERENCES `userinformation`(`username`),
    CONSTRAINT `pk_rentidtime` PRIMARY KEY (`fk_roomid`,`fk_userid`,`from`)
);