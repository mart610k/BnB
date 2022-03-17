USE bnb;

CREATE TABLE `usertype`(`usertype` VARCHAR(40) PRIMARY KEY NOT NULL);

INSERT INTO `usertype`(`usertype`) VALUES ("emploeyee"),("host");

CREATE TABLE `usertypeinformation`(
    `fk_usertype` VARCHAR(40) NOT NULL,
    `fk_username` VARCHAR(128) NOT NULL,
    FOREIGN KEY (`fk_usertype`) REFERENCES `usertype`(`usertype`),
    FOREIGN KEY (`fk_username`) REFERENCES `userinformation`(`username`),
    PRIMARY KEY (`fk_usertype`,`fk_username`) 
);


CREATE TABLE `requeststatus`(
    `statusname` VARCHAR(32) PRIMARY KEY NOT NULL
);

INSERT INTO `requeststatus`(`statusname`) VALUES ("accepted"),("rejected"), ("requested");

CREATE TABLE `userhostrequest`(
    `fk_username` VARCHAR(128) NOT NULL,
    `created_utc` TIMESTAMP DEFAULT NOW() NOT NULL,
    `requesttext` TEXT,
    `fk_requeststatus` VARCHAR(32) NOT NULL,
    FOREIGN KEY (`fk_username`) REFERENCES `userinformation`(`username`),
    FOREIGN KEY (`fk_requeststatus`) REFERENCES `requeststatus`(`statusname`),
    PRIMARY KEY (`fk_username`,`created_utc`)
);