-- Host: localhost:3306
-- Generation Time: Sep 25, 2016 at 10:48 PM
-- Server version: 5.6.33
-- PHP Version: 5.6.20

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

CREATE TABLE `tblUser` (
  `username` varchar(45) NOT NULL,
  `password` varchar(200) NOT NULL,
  `salt` varchar(200) NOT NULL,
  PRIMARY KEY (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblLogin`
--

INSERT INTO `tblUser` (`username`, `password`, `salt`) VALUES
('user', 'uLzOc9hqo47A75r1r9TE3ZctD3qmWEA4oQip4zfpgMg=', 'KUgMBBIZbPDsMiGUOc1UvQ=='),
('sharique', 'a3s0/ddJ+EMUdBs9sS3BqzFTrXck40P5/qiZ5DJdvN8=', 'HdGBAM5wrRRGV3YRQu1qDg==');



CREATE TABLE IF NOT EXISTS `tblCategory` (
  `categoryId` int(10) NOT NULL AUTO_INCREMENT,
  `categoryName` varchar(100) NOT NULL,
  PRIMARY KEY (`categoryId`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Dumping data for table `tblCategory`
--

INSERT INTO `tblCategory` (`categoryId`, `categoryName`) VALUES
(1, 'Technology'),
(2, 'School'),
(3, 'Play'),
(4, 'Data');



CREATE TABLE IF NOT EXISTS `tblLink` (
  `linkId` int(10) NOT NULL AUTO_INCREMENT,
  `categoryId`int(10) NOT NULL,
  `label` varchar(100) NOT NULL,
  `href` varchar(300) NOT NULL,
  `pinned` boolean NOT NULL,
  PRIMARY KEY (`linkId`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Dumping data for table `tblLink`
--

INSERT INTO `tblLink` (`categoryId`, `label`, `href`, `pinned`) VALUES
(1, 'AngularJS', 'https://angularjs.org/', 1),
(1, 'Java', 'https://www.java.com/en/', 0),
(1, 'NSCC', 'https://www.nscc.ca/', 1),
(4, 'Data', 'https://mail.google.com/mail/u/0/#inbox', 0);