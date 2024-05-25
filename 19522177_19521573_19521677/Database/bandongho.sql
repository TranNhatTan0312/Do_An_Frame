-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 13, 2022 at 06:47 PM
-- Server version: 10.4.21-MariaDB
-- PHP Version: 8.0.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `bandongho`
--

-- --------------------------------------------------------

--
-- Table structure for table `accounts`
--

CREATE TABLE `accounts` (
  `AccountID` int(11) NOT NULL,
  `Phone` varchar(12) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `Password` varchar(50) DEFAULT NULL,
  `Salt` char(10) DEFAULT NULL,
  `Active` bit(1) NOT NULL,
  `FullName` varchar(150) DEFAULT NULL,
  `RoleID` int(11) DEFAULT NULL,
  `LastLogin` datetime DEFAULT NULL,
  `createDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `attributes`
--

CREATE TABLE `attributes` (
  `AttributeID` int(11) NOT NULL,
  `Name` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `attributesprices`
--

CREATE TABLE `attributesprices` (
  `AttributesPriceID` int(11) NOT NULL,
  `AttributeID` int(11) DEFAULT NULL,
  `ProductID` int(11) DEFAULT NULL,
  `Price` int(11) DEFAULT NULL,
  `Active` bit(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE `categories` (
  `CatID` int(11) NOT NULL,
  `CatName` varchar(250) DEFAULT NULL,
  `Description` varchar(250) DEFAULT NULL,
  `ParentID` int(11) DEFAULT NULL,
  `Levels` int(11) DEFAULT NULL,
  `Ordering` int(11) DEFAULT NULL,
  `Published` bit(1) NOT NULL,
  `Thumb` varchar(250) DEFAULT NULL,
  `Title` varchar(250) DEFAULT NULL,
  `Alias` varchar(250) DEFAULT NULL,
  `MetaDesc` varchar(250) DEFAULT NULL,
  `MetaKey` varchar(250) DEFAULT NULL,
  `Cover` varchar(255) DEFAULT NULL,
  `SchemaMarkup` varchar(250) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`CatID`, `CatName`, `Description`, `ParentID`, `Levels`, `Ordering`, `Published`, `Thumb`, `Title`, `Alias`, `MetaDesc`, `MetaKey`, `Cover`, `SchemaMarkup`) VALUES
(1, 'Casio', 'Đây là đồng hồ thuộc thương hiệu Casio', NULL, NULL, NULL, b'1', NULL, NULL, 'Casio', NULL, NULL, NULL, NULL),
(3, 'Thuỵ Sĩ', 'Dong ho sieu nhan 123', NULL, NULL, NULL, b'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(5, 'Citizen', 'Citizen', NULL, NULL, NULL, b'1', 'netizen.png', 'Citizen', 'citizen', NULL, NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `customers`
--

CREATE TABLE `customers` (
  `CustomerID` int(11) NOT NULL,
  `FullName` varchar(255) DEFAULT NULL,
  `Birthday` datetime DEFAULT NULL,
  `Avatar` varchar(255) DEFAULT NULL,
  `Address` varchar(255) DEFAULT NULL,
  `Email` char(150) DEFAULT NULL,
  `Phone` varchar(12) DEFAULT NULL,
  `LocationID` int(11) DEFAULT NULL,
  `District` int(11) DEFAULT NULL,
  `Ward` int(11) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `Password` varchar(50) DEFAULT NULL,
  `Salt` char(8) DEFAULT NULL,
  `LastLogin` datetime DEFAULT NULL,
  `Active` bit(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `customers`
--

INSERT INTO `customers` (`CustomerID`, `FullName`, `Birthday`, `Avatar`, `Address`, `Email`, `Phone`, `LocationID`, `District`, `Ward`, `CreateDate`, `Password`, `Salt`, `LastLogin`, `Active`) VALUES
(28, 'Tan Tran', NULL, NULL, NULL, 'tantran@gmail.com', '0123456789', NULL, NULL, NULL, '2022-01-13 15:36:34', 'a811cdd83627c2eb66a8e182b1627fb6', 'k4(qn', NULL, b'1'),
(29, 'Trần Nhật Tân', NULL, NULL, NULL, 'kakacm2015@gmail.com', '0843372090', NULL, NULL, NULL, '2022-01-13 19:44:14', '24b88dd19d65e6bd3d7341fa11cdbdcf', '54g^z', NULL, b'1');

-- --------------------------------------------------------

--
-- Table structure for table `locations`
--

CREATE TABLE `locations` (
  `LocationID` int(11) NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Parent` int(11) DEFAULT NULL,
  `Levels` int(11) DEFAULT NULL,
  `Slug` varchar(100) DEFAULT NULL,
  `NameWithType` varchar(100) DEFAULT NULL,
  `Type` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `locations`
--

INSERT INTO `locations` (`LocationID`, `Name`, `Parent`, `Levels`, `Slug`, `NameWithType`, `Type`) VALUES
(1, 'Ca Mau', NULL, NULL, NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `orderdetails`
--

CREATE TABLE `orderdetails` (
  `OrderDetailID` int(11) NOT NULL,
  `OrderID` int(11) DEFAULT NULL,
  `ProductID` int(11) DEFAULT NULL,
  `OrderNumber` int(11) DEFAULT NULL,
  `Amount` int(11) DEFAULT NULL,
  `Discount` int(11) DEFAULT NULL,
  `TotalMoney` int(11) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `Price` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `OrderID` int(11) NOT NULL,
  `CustomerID` int(11) DEFAULT NULL,
  `OrderDate` datetime DEFAULT NULL,
  `ShipDate` datetime DEFAULT NULL,
  `TransactStatusID` int(11) NOT NULL,
  `Deleted` bit(1) NOT NULL,
  `Paid` bit(1) NOT NULL,
  `PaymentDate` datetime DEFAULT NULL,
  `TotalMoney` int(11) NOT NULL,
  `PaymentID` int(11) DEFAULT NULL,
  `Note` varchar(250) DEFAULT NULL,
  `Address` varchar(250) DEFAULT NULL,
  `LocationID` int(11) DEFAULT NULL,
  `District` int(11) DEFAULT NULL,
  `Ward` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `pages`
--

CREATE TABLE `pages` (
  `PageID` int(11) NOT NULL,
  `PageName` varchar(250) DEFAULT NULL,
  `Contents` varchar(250) DEFAULT NULL,
  `Thumb` varchar(250) DEFAULT NULL,
  `Published` bit(1) NOT NULL,
  `Title` varchar(250) DEFAULT NULL,
  `MetaDesc` varchar(250) DEFAULT NULL,
  `MetaKey` varchar(250) DEFAULT NULL,
  `Alias` varchar(250) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `Ordering` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `pages`
--

INSERT INTO `pages` (`PageID`, `PageName`, `Contents`, `Thumb`, `Published`, `Title`, `MetaDesc`, `MetaKey`, `Alias`, `CreatedDate`, `Ordering`) VALUES
(14, 'Hướng dẫn mua hàng', '<p>234</p>', 'huong-dan-mua-hang.png', b'1', NULL, NULL, NULL, 'huong-dan-mua-hang', NULL, NULL),
(20, 'Test', '<p>Test123</p>', 'test.png', b'1', '1', '1', '1', 'test', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `ProductID` int(11) NOT NULL,
  `ProductName` varchar(255) NOT NULL,
  `ShortDesc` varchar(255) DEFAULT NULL,
  `Description` varchar(250) DEFAULT NULL,
  `CatID` int(11) DEFAULT NULL,
  `Price` int(11) DEFAULT NULL,
  `Discount` int(11) DEFAULT NULL,
  `Thumb` varchar(255) DEFAULT NULL,
  `Video` varchar(255) DEFAULT NULL,
  `DateCreated` datetime DEFAULT NULL,
  `DateModified` datetime DEFAULT NULL,
  `BestSellers` bit(1) NOT NULL,
  `HomeFlag` bit(1) NOT NULL,
  `Active` bit(1) NOT NULL,
  `Tags` varchar(250) DEFAULT NULL,
  `Title` varchar(255) DEFAULT NULL,
  `Alias` varchar(255) DEFAULT NULL,
  `MetaDesc` varchar(255) DEFAULT NULL,
  `MetaKey` varchar(255) DEFAULT NULL,
  `UnitsInStock` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`ProductID`, `ProductName`, `ShortDesc`, `Description`, `CatID`, `Price`, `Discount`, `Thumb`, `Video`, `DateCreated`, `DateModified`, `BestSellers`, `HomeFlag`, `Active`, `Tags`, `Title`, `Alias`, `MetaDesc`, `MetaKey`, `UnitsInStock`) VALUES
(32, 'Đồng Hồ Siêu Nhân Thế Hệ Mới', '123', '123', 1, 12345, NULL, 'dong-ho-sieu-nhan-the-he-moi.png', NULL, '2022-01-11 16:05:02', '2022-01-11 16:05:02', b'0', b'0', b'1', NULL, '1', 'dong-ho-sieu-nhan-the-he-moi', '1', '1', 1),
(33, 'Casio-lq-139bmv-1bldf-den-7-600x600', NULL, NULL, 1, 1200000, NULL, 'casio-lq-139bmv-1bldf-den-7-600x600.jpg', NULL, '2022-01-12 13:21:20', '2022-01-12 13:21:20', b'0', b'0', b'1', NULL, NULL, 'casio-lq-139bmv-1bldf-den-7-600x600', NULL, NULL, 1),
(34, 'Casio-ltp-1095q-1a-den-1-1-600x600', NULL, NULL, 1, 2000000, NULL, 'casio-ltp-1095q-1a-den-1-1-600x600.jpg', NULL, '2022-01-12 13:23:36', '2022-01-12 14:06:38', b'1', b'1', b'1', NULL, NULL, 'casio-ltp-1095q-1a-den-1-1-600x600', NULL, NULL, 1),
(35, 'Casio-ltp-1183q-7adf-nu-600x600', 'casio-ltp-1183q-7adf-nu-600x600', 'casio-ltp-1183q-7adf-nu-600x600', 1, 1200000, NULL, 'casio-ltp-1183q-7adf-nu-600x600.jpg', NULL, '2022-01-12 14:03:29', '2022-01-12 14:03:29', b'0', b'0', b'1', NULL, 'casio-ltp-1183q-7adf-nu-600x600', 'casio-ltp-1183q-7adf-nu-600x600', 'casio-ltp-1183q-7adf-nu-600x600', 'casio-ltp-1183q-7adf-nu-600x600', 1),
(36, 'Casio-ltp-v001l-7budf-den-600x600', 'casio-ltp-v001l-7budf-den-600x600', 'casio-ltp-v001l-7budf-den-600x600', 1, 1000000, NULL, 'casio-ltp-v001l-7budf-den-600x600.jpg', NULL, '2022-01-12 14:03:58', '2022-01-12 14:03:58', b'0', b'0', b'1', NULL, 'casio-ltp-v001l-7budf-den-600x600', 'casio-ltp-v001l-7budf-den-600x600', 'casio-ltp-v001l-7budf-den-600x600', 'casio-ltp-v001l-7budf-den-600x600', 1),
(37, 'Casio-ltp-v002gl-1budf-nau-600x600', 'casio-ltp-v002gl-1budf-nau-600x600', 'casio-ltp-v002gl-1budf-nau-600x600', 1, 1230000, NULL, 'casio-ltp-v002gl-1budf-nau-600x600.jpg', NULL, '2022-01-12 14:04:58', '2022-01-12 14:04:58', b'0', b'0', b'1', NULL, 'casio-ltp-v002gl-1budf-nau-600x600', 'casio-ltp-v002gl-1budf-nau-600x600', 'casio-ltp-v002gl-1budf-nau-600x600', 'casio-ltp-v002gl-1budf-nau-600x600', 1),
(38, 'Casio-mrw-200h-7bvdf-den-6-600x600', 'casio-mrw-200h-7bvdf-den-6-600x600', 'casio-mrw-200h-7bvdf-den-6-600x600', 1, 5000000, NULL, 'casio-mrw-200h-7bvdf-den-6-600x600.jpg', NULL, '2022-01-12 14:11:47', '2022-01-12 14:11:47', b'0', b'0', b'1', NULL, 'casio-mrw-200h-7bvdf-den-6-600x600', 'casio-mrw-200h-7bvdf-den-6-600x600', 'casio-mrw-200h-7bvdf-den-6-600x600', 'casio-mrw-200h-7bvdf-den-6-600x600', 1),
(39, 'Casio-mtp-1094e-7adf-nam-013520-113505-600x600', 'casio-mtp-1094e-7adf-nam-013520-113505-600x600', 'casio-mtp-1094e-7adf-nam-013520-113505-600x600', 1, 5000000, NULL, 'casio-mtp-1094e-7adf-nam-013520-113505-600x600.jpg', NULL, '2022-01-12 14:25:34', '2022-01-12 14:25:34', b'0', b'0', b'1', NULL, 'casio-mtp-1094e-7adf-nam-013520-113505-600x600', 'casio-mtp-1094e-7adf-nam-013520-113505-600x600', 'casio-mtp-1094e-7adf-nam-013520-113505-600x600', 'casio-mtp-1094e-7adf-nam-013520-113505-600x600', 1),
(40, 'Casio-mtp-1370d-1a1vdf-nam-thum-600x600', 'casio-mtp-1370d-1a1vdf-nam-thum-600x600', 'casio-mtp-1370d-1a1vdf-nam-thum-600x600', 1, 5000000, NULL, 'casio-mtp-1370d-1a1vdf-nam-thum-600x600.jpg', NULL, '2022-01-12 14:26:15', '2022-01-12 14:26:15', b'0', b'0', b'1', NULL, 'casio-mtp-1370d-1a1vdf-nam-thum-600x600', 'casio-mtp-1370d-1a1vdf-nam-thum-600x600', 'casio-mtp-1370d-1a1vdf-nam-thum-600x600', 'casio-mtp-1370d-1a1vdf-nam-thum-600x600', 1),
(41, 'Casio-mtp-1370d-9avdf-nam-thum-600x600', 'casio-mtp-1370d-9avdf-nam-thum-600x600', 'casio-mtp-1370d-9avdf-nam-thum-600x600', 1, 5000000, NULL, 'casio-mtp-1370d-9avdf-nam-thum-600x600.jpg', NULL, '2022-01-12 14:26:53', '2022-01-12 14:26:53', b'0', b'0', b'1', NULL, 'casio-mtp-1370d-9avdf-nam-thum-600x600', 'casio-mtp-1370d-9avdf-nam-thum-600x600', 'casio-mtp-1370d-9avdf-nam-thum-600x600', 'casio-mtp-1370d-9avdf-nam-thum-600x600', 2),
(42, 'Casio-mtp-1370l-1avdf-nam-avatar-1-600x600', 'casio-mtp-1370l-1avdf-nam-avatar-1-600x600', 'casio-mtp-1370l-1avdf-nam-avatar-1-600x600', 1, 5000000, NULL, 'casio-mtp-1370l-1avdf-nam-avatar-1-600x600.jpg', NULL, '2022-01-12 14:27:27', '2022-01-13 19:53:02', b'0', b'0', b'1', NULL, 'casio-mtp-1370l-1avdf-nam-avatar-1-600x600', 'casio-mtp-1370l-1avdf-nam-avatar-1-600x600', 'casio-mtp-1370l-1avdf-nam-avatar-1-600x600', 'casio-mtp-1370l-1avdf-nam-avatar-1-600x600', 50),
(43, 'Citizen-bi1055-52e-nam-avatar-1-600x600', 'citizen-bi1055-52e-nam-avatar-1-600x600', 'citizen-bi1055-52e-nam-avatar-1-600x600', 5, 1200000, NULL, 'citizen-bi1055-52e-nam-avatar-1-600x600.jpg', NULL, '2022-01-12 16:15:27', '2022-01-12 16:15:27', b'0', b'0', b'1', NULL, 'citizen-bi1055-52e-nam-avatar-1-600x600', 'citizen-bi1055-52e-nam-avatar-1-600x600', 'citizen-bi1055-52e-nam-avatar-1-600x600', 'citizen-bi1055-52e-nam-avatar-1-600x600', 1),
(44, 'Đồng Hồ Thời Trang Citizen', 'Đồng hồ thời trang Citizen', 'Đồng hồ thời trang Citizen', 5, 5000000, NULL, 'dong-ho-thoi-trang-citizen.jpg', NULL, '2022-01-13 19:55:35', '2022-01-13 19:55:35', b'0', b'0', b'1', NULL, 'Đồng hồ thời trang Citizen', 'dong-ho-thoi-trang-citizen', 'Đồng hồ thời trang Citizen', 'Đồng hồ thời trang Citizen', 5);

-- --------------------------------------------------------

--
-- Table structure for table `quangcaos`
--

CREATE TABLE `quangcaos` (
  `QuangCaoID` int(11) NOT NULL,
  `SubTitle` varchar(150) DEFAULT NULL,
  `Title` varchar(150) DEFAULT NULL,
  `ImageBG` varchar(250) DEFAULT NULL,
  `ImageProduct` varchar(250) DEFAULT NULL,
  `UrlLink` varchar(250) DEFAULT NULL,
  `Active` bit(1) NOT NULL,
  `CreateDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `roles`
--

CREATE TABLE `roles` (
  `RoleID` int(11) NOT NULL,
  `RoleName` varchar(50) DEFAULT NULL,
  `Description` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `roles`
--

INSERT INTO `roles` (`RoleID`, `RoleName`, `Description`) VALUES
(1, 'Admin', 'Quyền lớn nhất'),
(2, 'Staff', 'Nhân viên');

-- --------------------------------------------------------

--
-- Table structure for table `shippers`
--

CREATE TABLE `shippers` (
  `ShipperID` int(11) NOT NULL,
  `ShipperName` varchar(150) DEFAULT NULL,
  `Phone` char(10) DEFAULT NULL,
  `Company` varchar(150) DEFAULT NULL,
  `ShipDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `tindangs`
--

CREATE TABLE `tindangs` (
  `PostID` int(11) NOT NULL,
  `Title` varchar(255) DEFAULT NULL,
  `SContents` varchar(255) DEFAULT NULL,
  `Contents` varchar(250) DEFAULT NULL,
  `Thumb` varchar(255) DEFAULT NULL,
  `Published` bit(1) NOT NULL,
  `Alias` varchar(255) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `Author` varchar(255) DEFAULT NULL,
  `AccountID` int(11) DEFAULT NULL,
  `Tags` varchar(250) DEFAULT NULL,
  `CatID` int(11) DEFAULT NULL,
  `isHot` bit(1) NOT NULL,
  `isNewfeed` bit(1) NOT NULL,
  `MetaKey` varchar(255) DEFAULT NULL,
  `MetaDesc` varchar(255) DEFAULT NULL,
  `_Views` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tindangs`
--

INSERT INTO `tindangs` (`PostID`, `Title`, `SContents`, `Contents`, `Thumb`, `Published`, `Alias`, `CreatedDate`, `Author`, `AccountID`, `Tags`, `CatID`, `isHot`, `isNewfeed`, `MetaKey`, `MetaDesc`, `_Views`) VALUES
(673, 'Bán đồng hồ 1235', NULL, NULL, 'ban-dong-ho-1235.jpg', b'1', 'ban-dong-ho-1235', '2022-01-11 22:04:59', NULL, NULL, NULL, NULL, b'1', b'1', NULL, '123', NULL),
(674, 'Bán đồng hồ 123589', NULL, NULL, 'ban-dong-ho-123589.jpg', b'1', 'ban-dong-ho-123589', '2022-01-11 22:04:55', NULL, NULL, NULL, NULL, b'1', b'1', NULL, NULL, NULL),
(675, 'Đồng hồ 123', NULL, NULL, 'dong-ho-123.jpg', b'1', 'dong-ho-123', '2022-01-11 16:32:51', NULL, NULL, NULL, NULL, b'0', b'0', NULL, NULL, NULL),
(676, 'Đồng hồ 123', NULL, NULL, 'dong-ho-123.jpg', b'1', 'dong-ho-123', '2022-01-11 16:32:51', NULL, NULL, NULL, NULL, b'0', b'0', NULL, NULL, NULL),
(677, 'Đồng hồ 123', NULL, NULL, 'dong-ho-123.jpg', b'1', 'dong-ho-123', '2022-01-11 16:32:51', NULL, NULL, NULL, NULL, b'0', b'0', NULL, NULL, NULL),
(678, 'Đồng hồ 123', NULL, NULL, 'dong-ho-123.jpg', b'1', 'dong-ho-123', '2022-01-11 16:32:51', NULL, NULL, NULL, NULL, b'0', b'0', NULL, NULL, NULL),
(679, 'Đồng hồ 123', NULL, NULL, 'dong-ho-123.jpg', b'1', 'dong-ho-123', '2022-01-11 16:32:51', NULL, NULL, NULL, NULL, b'0', b'0', NULL, NULL, NULL),
(680, 'Đồng hồ 123', NULL, NULL, 'dong-ho-123.jpg', b'1', 'dong-ho-123', '2022-01-11 16:32:51', NULL, NULL, NULL, NULL, b'0', b'0', NULL, NULL, NULL),
(681, 'Đồng hồ 123', NULL, NULL, 'dong-ho-123.jpg', b'1', 'dong-ho-123', '2022-01-11 16:32:51', NULL, NULL, NULL, NULL, b'0', b'1', NULL, NULL, NULL),
(682, 'Đồng hồ 123', NULL, NULL, 'dong-ho-123.jpg', b'1', 'dong-ho-123', '2022-01-11 16:32:51', NULL, NULL, NULL, NULL, b'0', b'0', NULL, NULL, NULL),
(685, 'Tin tức 1', NULL, 'đồng hồ siêu nhân\r\nđồng hồ siêu nhânđồng hồ siêu nhân\r\nđồng hồ siêu nhân\r\nđồng hồ siêu nhân\r\nđồng hồ siêu nhân\r\nđồng hồ siêu nhân\r\nđồng hồ siêu nhân\r\nđồng hồ siêu nhân\r\nđồng hồ siêu nhân', 'tin-tuc-1.jpg', b'1', 'tin-tuc-1', '2022-01-12 11:28:58', NULL, NULL, NULL, NULL, b'1', b'0', '1', '1', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `transactstatus`
--

CREATE TABLE `transactstatus` (
  `TransactStatusID` int(11) NOT NULL,
  `Status` varchar(50) DEFAULT NULL,
  `Description` varchar(250) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `accounts`
--
ALTER TABLE `accounts`
  ADD PRIMARY KEY (`AccountID`),
  ADD KEY `FK_Accounts_Roles` (`RoleID`);

--
-- Indexes for table `attributes`
--
ALTER TABLE `attributes`
  ADD PRIMARY KEY (`AttributeID`);

--
-- Indexes for table `attributesprices`
--
ALTER TABLE `attributesprices`
  ADD PRIMARY KEY (`AttributesPriceID`),
  ADD KEY `FK_AttributesPrices_Attributes` (`AttributeID`),
  ADD KEY `FK_AttributesPrices_Products` (`ProductID`);

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`CatID`);

--
-- Indexes for table `customers`
--
ALTER TABLE `customers`
  ADD PRIMARY KEY (`CustomerID`),
  ADD KEY `LocationID` (`LocationID`);

--
-- Indexes for table `locations`
--
ALTER TABLE `locations`
  ADD PRIMARY KEY (`LocationID`);

--
-- Indexes for table `orderdetails`
--
ALTER TABLE `orderdetails`
  ADD PRIMARY KEY (`OrderDetailID`),
  ADD KEY `FK_OrderDetails_Orders` (`OrderID`),
  ADD KEY `FK_OrderDetails_Products` (`ProductID`);

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`OrderID`),
  ADD KEY `FK_Orders_Customers` (`CustomerID`),
  ADD KEY `FK_Orders_TransactStatus` (`TransactStatusID`);

--
-- Indexes for table `pages`
--
ALTER TABLE `pages`
  ADD PRIMARY KEY (`PageID`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`ProductID`),
  ADD KEY `FK_Products_Categories` (`CatID`);

--
-- Indexes for table `quangcaos`
--
ALTER TABLE `quangcaos`
  ADD PRIMARY KEY (`QuangCaoID`);

--
-- Indexes for table `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`RoleID`);

--
-- Indexes for table `shippers`
--
ALTER TABLE `shippers`
  ADD PRIMARY KEY (`ShipperID`);

--
-- Indexes for table `tindangs`
--
ALTER TABLE `tindangs`
  ADD PRIMARY KEY (`PostID`);

--
-- Indexes for table `transactstatus`
--
ALTER TABLE `transactstatus`
  ADD PRIMARY KEY (`TransactStatusID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `accounts`
--
ALTER TABLE `accounts`
  MODIFY `AccountID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `attributes`
--
ALTER TABLE `attributes`
  MODIFY `AttributeID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `attributesprices`
--
ALTER TABLE `attributesprices`
  MODIFY `AttributesPriceID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `CatID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `customers`
--
ALTER TABLE `customers`
  MODIFY `CustomerID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT for table `orderdetails`
--
ALTER TABLE `orderdetails`
  MODIFY `OrderDetailID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `OrderID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `pages`
--
ALTER TABLE `pages`
  MODIFY `PageID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `ProductID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=45;

--
-- AUTO_INCREMENT for table `quangcaos`
--
ALTER TABLE `quangcaos`
  MODIFY `QuangCaoID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `roles`
--
ALTER TABLE `roles`
  MODIFY `RoleID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `shippers`
--
ALTER TABLE `shippers`
  MODIFY `ShipperID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tindangs`
--
ALTER TABLE `tindangs`
  MODIFY `PostID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=687;

--
-- AUTO_INCREMENT for table `transactstatus`
--
ALTER TABLE `transactstatus`
  MODIFY `TransactStatusID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `accounts`
--
ALTER TABLE `accounts`
  ADD CONSTRAINT `FK_Accounts_Roles` FOREIGN KEY (`RoleID`) REFERENCES `roles` (`RoleID`);

--
-- Constraints for table `attributesprices`
--
ALTER TABLE `attributesprices`
  ADD CONSTRAINT `FK_AttributesPrices_Attributes` FOREIGN KEY (`AttributeID`) REFERENCES `attributes` (`AttributeID`),
  ADD CONSTRAINT `FK_AttributesPrices_Products` FOREIGN KEY (`ProductID`) REFERENCES `products` (`ProductID`);

--
-- Constraints for table `customers`
--
ALTER TABLE `customers`
  ADD CONSTRAINT `customers_ibfk_1` FOREIGN KEY (`LocationID`) REFERENCES `locations` (`LocationID`);

--
-- Constraints for table `orderdetails`
--
ALTER TABLE `orderdetails`
  ADD CONSTRAINT `FK_OrderDetails_Orders` FOREIGN KEY (`OrderID`) REFERENCES `orders` (`OrderID`),
  ADD CONSTRAINT `FK_OrderDetails_Products` FOREIGN KEY (`ProductID`) REFERENCES `products` (`ProductID`);

--
-- Constraints for table `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `FK_Orders_Customers` FOREIGN KEY (`CustomerID`) REFERENCES `customers` (`CustomerID`),
  ADD CONSTRAINT `FK_Orders_TransactStatus` FOREIGN KEY (`TransactStatusID`) REFERENCES `transactstatus` (`TransactStatusID`);

--
-- Constraints for table `products`
--
ALTER TABLE `products`
  ADD CONSTRAINT `FK_Products_Categories` FOREIGN KEY (`CatID`) REFERENCES `categories` (`CatID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
