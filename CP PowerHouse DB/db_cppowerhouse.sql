-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 04, 2023 at 12:52 PM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_cppowerhouse`
--

-- --------------------------------------------------------

--
-- Table structure for table `favourite_contest`
--

CREATE TABLE `favourite_contest` (
  `id` int(10) NOT NULL,
  `name` varchar(300) NOT NULL,
  `startTimeSeconds` int(20) NOT NULL,
  `durationSeconds` int(20) NOT NULL,
  `phase` varchar(30) NOT NULL,
  `platform` varchar(30) NOT NULL,
  `startTimeSecondsView` varchar(150) NOT NULL,
  `durationSecondsView` varchar(150) NOT NULL,
  `link` varchar(300) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `favourite_contest`
--

INSERT INTO `favourite_contest` (`id`, `name`, `startTimeSeconds`, `durationSeconds`, `phase`, `platform`, `startTimeSecondsView`, `durationSecondsView`, `link`) VALUES
(47614715, '[ctftime.org] M*CTF 2023 Quals', 1699700400, 86400, 'BEFORE', 'ctftime.org', '11 November 2023, 05:00 PM', '12:00', 'https://ctftime.org/event/2096/'),
(47614333, '[leetcode.com] Biweekly Contest 117', 1699713000, 5400, 'BEFORE', 'leetcode.com', '11 November 2023, 08:30 PM', '01:30', 'https://leetcode.com/contest/biweekly-contest-117'),
(47614750, '[facebook.com/hackercup] Final Round 2023', 1702130400, 10800, 'BEFORE', 'facebook.com/hackercup', '09 December 2023, 08:00 PM', '03:00', 'https://facebook.com/codingcompetitions/hacker-cup/2023/final-round'),
(1896, 'CodeTON Round 7 (Div. 1 + Div. 2, Rated, Prizes!)', 1700922900, 9000, 'BEFORE', 'CodeForces', '25 November 2023, 08:35 PM', '02:30', 'https://codeforces.com/contests/1896'),
(47614332, '[leetcode.com] Weekly Contest 371', 1699756200, 5400, 'BEFORE', 'leetcode.com', '12 November 2023, 08:30 AM', '01:30', 'https://leetcode.com/contest/weekly-contest-371'),
(1903, 'Codeforces Round 912 (Div. 2)', 1701362100, 8100, 'BEFORE', 'CodeForces', '30 November 2023, 10:35 PM', '02:15', 'https://codeforces.com/contests/1903'),
(1903, 'Codeforces Round 912 (Div. 2)', 1701362100, 8100, 'BEFORE', 'CodeForces', '30 November 2023, 10:35 PM', '02:15', 'https://codeforces.com/contests/1903'),
(1902, 'Educational Codeforces Round 159 (Rated for Div. 2)', 1701614100, 7200, 'BEFORE', 'CodeForces', '03 December 2023, 08:35 PM', '02:00', 'https://codeforces.com/contests/1902');

-- --------------------------------------------------------

--
-- Table structure for table `friends`
--

CREATE TABLE `friends` (
  `platform` varchar(30) NOT NULL,
  `username` varchar(250) NOT NULL,
  `url` varchar(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `friends`
--

INSERT INTO `friends` (`platform`, `username`, `url`) VALUES
('Codeforces', 'mdtuhinhasnat', 'https://codeforces.com/profile/mdtuhinhasnat'),
('Codeforces', 'sifat.sharif', 'https://codeforces.com/profile/sifat.sharif'),
('Codeforces', '101rakibulhasan', 'https://codeforces.com/profile/101rakibulhasan'),
('Codeforces', 'abidurabid', 'https://codeforces.com/profile/abidurabid'),
('Light OJ', 'sifat_sharif', 'http://lightoj.com/user/sifat_sharif'),
('CodeChef', 'rakibul_hasan', 'https://www.codechef.com/users/rakibul_hasan'),
('UVA', 'isratjahan', 'https://uhunt.onlinejudge.org/id/isratjahan');

-- --------------------------------------------------------

--
-- Table structure for table `problemset`
--

CREATE TABLE `problemset` (
  `platform` varchar(30) NOT NULL,
  `name` varchar(250) NOT NULL,
  `url` varchar(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `problemset`
--

INSERT INTO `problemset` (`platform`, `name`, `url`) VALUES
('LeetCode', '2. Add Two Numbers', 'https://leetcode.com/problems/add-two-numbers/'),
('CodeForces', 'B. Chemistry', 'https://codeforces.com/problemset/problem/1883/B'),
('CodeChef', 'Alternating Work Days', 'https://www.codechef.com/practice/course/2to3stars/LP2TO301/problems/ALTER'),
('Hackerrank', 'Say \"Hello, World!\" With C++', 'https://www.hackerrank.com/challenges/cpp-hello-world/');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
