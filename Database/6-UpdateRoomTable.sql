USE BNB;


ALTER TABLE `Room` ADD `Price` INT DEFAULT 0 NOT NULL;

DROP TABLE `StatusRoom`;

DROP TABLE `Status`;

CREATE TABLE `Rent`(
    `RoomId` INT, 
    `From` DATE NOT NULL, 
    `To` DATE NOT NULL, 
    `UserId` VARCHAR(128), 
    `Accepted` BOOLEAN,
    FOREIGN KEY(`RoomId`) REFERENCES `Room`(`RoomId`), 
    FOREIGN KEY(`UserId`) REFERENCES `UserInformation`(`Username`),
    CONSTRAINT `PK_RentIDTime` PRIMARY KEY (`RoomId`,`UserId`,`From`)
);