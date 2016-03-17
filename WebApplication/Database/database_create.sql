-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: 17-Mar-2016 às 04:31
-- Versão do servidor: 5.6.17
-- PHP Version: 5.5.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `sognare`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `Customer`
--

CREATE TABLE IF NOT EXISTS `Customer` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `user_id` int(10) unsigned DEFAULT NULL,
  `name` varchar(100) NOT NULL,
  `phone` varchar(15) DEFAULT NULL,
  `birth_date` date DEFAULT NULL,
  `enabled` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `phone` (`phone`),
  KEY `user_id_FK` (`user_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=14 ;

-- --------------------------------------------------------

--
-- Estrutura da tabela `CustomerAddress`
--

CREATE TABLE IF NOT EXISTS `CustomerAddress` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `customer_id` int(10) unsigned NOT NULL,
  `zip_code` varchar(8) NOT NULL,
  `address` varchar(255) NOT NULL,
  `number` int(11) DEFAULT NULL,
  `complement` varchar(50) DEFAULT NULL,
  `neighborhood` varchar(50) NOT NULL,
  `city` varchar(50) NOT NULL,
  `acronym_city` char(2) NOT NULL,
  `reference_point` varchar(100) DEFAULT NULL,
  `latitude` decimal(10,8) DEFAULT NULL,
  `longitude` decimal(10,8) DEFAULT NULL,
  `enabled` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  KEY `customer_id_FK` (`customer_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=7 ;

-- --------------------------------------------------------

--
-- Estrutura da tabela `Drink`
--

CREATE TABLE IF NOT EXISTS `Drink` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `drink_type_id` int(10) unsigned NOT NULL,
  `name` varchar(50) NOT NULL,
  `price` decimal(15,2) NOT NULL,
  `image` varchar(255) DEFAULT NULL,
  `enabled` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`),
  KEY `drink_type_id_FK` (`drink_type_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estrutura da tabela `DrinkType`
--

CREATE TABLE IF NOT EXISTS `DrinkType` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estrutura da tabela `Employer`
--

CREATE TABLE IF NOT EXISTS `Employer` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `user_id` int(10) unsigned NOT NULL,
  `name` varchar(255) NOT NULL,
  `phone` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `user_id_FK` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estrutura da tabela `Ingredient`
--

CREATE TABLE IF NOT EXISTS `Ingredient` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estrutura da tabela `Order`
--

CREATE TABLE IF NOT EXISTS `Order` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `balcony` tinyint(1) NOT NULL DEFAULT '0',
  `payment_type_id` int(11) NOT NULL,
  `customer_address_id` int(10) unsigned DEFAULT NULL,
  `price` decimal(15,2) NOT NULL,
  `discount` decimal(15,2) NOT NULL DEFAULT '0.00',
  `final_price` decimal(15,2) NOT NULL,
  `change` decimal(15,2) NOT NULL DEFAULT '0.00',
  `delivery_price` decimal(15,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (`id`),
  KEY `payment_type_id_FK` (`payment_type_id`),
  KEY `orders_customer_address_id_FK` (`customer_address_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estrutura da tabela `OrderLog`
--

CREATE TABLE IF NOT EXISTS `OrderLog` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `order_id` int(10) unsigned NOT NULL,
  `order_status_id` int(10) unsigned NOT NULL,
  `user_id` int(10) unsigned NOT NULL,
  `note` varchar(100) DEFAULT NULL,
  `order_log_datetime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `order_id_order_status_id_UNIQUE` (`order_id`,`order_status_id`),
  KEY `order_id_FK` (`order_id`),
  KEY `order_status_id_FK` (`order_status_id`),
  KEY `user_id_FK` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estrutura da tabela `OrderPizza`
--

CREATE TABLE IF NOT EXISTS `OrderPizza` (
  `order_id` int(10) unsigned NOT NULL,
  `pizza_id` int(10) unsigned zerofill NOT NULL,
  PRIMARY KEY (`order_id`,`pizza_id`),
  KEY `pizza_id_FK` (`pizza_id`),
  KEY `order_id_FK` (`order_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `OrdersDrink`
--

CREATE TABLE IF NOT EXISTS `OrdersDrink` (
  `order_id` int(10) unsigned NOT NULL,
  `drink_id` int(10) unsigned NOT NULL,
  PRIMARY KEY (`order_id`,`drink_id`),
  KEY `drink_id_FK` (`drink_id`),
  KEY `order_id_FK` (`order_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `OrderStatus`
--

CREATE TABLE IF NOT EXISTS `OrderStatus` (
  `id` int(10) unsigned NOT NULL,
  `description` varchar(50) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `description_UNIQUE` (`description`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `OrderStatus`
--

INSERT INTO `OrderStatus` (`id`, `description`) VALUES
(1, 'Aberto'),
(6, 'Cancelado'),
(4, 'Em entrega'),
(2, 'Em produção'),
(5, 'Entregue'),
(7, 'Fechado'),
(3, 'Pronto');

-- --------------------------------------------------------

--
-- Estrutura da tabela `PaymentType`
--

CREATE TABLE IF NOT EXISTS `PaymentType` (
  `id` int(11) NOT NULL,
  `name` varchar(50) NOT NULL,
  `enabled` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `PaymentType`
--

INSERT INTO `PaymentType` (`id`, `name`, `enabled`) VALUES
(1, 'Dinheiro', 0);

-- --------------------------------------------------------

--
-- Estrutura da tabela `Pizza`
--

CREATE TABLE IF NOT EXISTS `Pizza` (
  `id` int(10) unsigned zerofill NOT NULL,
  `pizza_flavor_id` int(10) unsigned NOT NULL,
  `pizza_size_id` int(10) unsigned NOT NULL,
  `price` decimal(15,2) NOT NULL,
  `enabled` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  KEY `pizza_size_id_FK` (`pizza_size_id`),
  KEY `pizza_flavor_id_FK` (`pizza_flavor_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `PizzaFlavor`
--

CREATE TABLE IF NOT EXISTS `PizzaFlavor` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `image` varchar(255) DEFAULT NULL,
  `enabled` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estrutura da tabela `PizzaFlavorIngredient`
--

CREATE TABLE IF NOT EXISTS `PizzaFlavorIngredient` (
  `pizza_flavor_id` int(10) unsigned NOT NULL,
  `ingredient_id` int(10) unsigned NOT NULL,
  PRIMARY KEY (`pizza_flavor_id`,`ingredient_id`),
  KEY `ingredient_id_FK` (`ingredient_id`),
  KEY `pizza_flavor_id_FK` (`pizza_flavor_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `PizzaSize`
--

CREATE TABLE IF NOT EXISTS `PizzaSize` (
  `id` int(10) unsigned NOT NULL,
  `name` varchar(50) NOT NULL,
  `size` int(11) NOT NULL,
  `slices` int(11) NOT NULL,
  `price` decimal(15,2) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `PizzaSize`
--

INSERT INTO `PizzaSize` (`id`, `name`, `size`, `slices`, `price`) VALUES
(1, 'Média', 30, 8, '0.00'),
(2, 'Família', 40, 10, '0.00'),
(3, 'Gigante', 45, 12, '0.00');

-- --------------------------------------------------------

--
-- Estrutura da tabela `Profile`
--

CREATE TABLE IF NOT EXISTS `Profile` (
  `id` int(10) unsigned NOT NULL,
  `name` varchar(20) NOT NULL,
  `level` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `Profile`
--

INSERT INTO `Profile` (`id`, `name`, `level`) VALUES
(1, 'Cliente', 10),
(2, 'Entregador', 20),
(3, 'Atendente', 30),
(4, 'Gerente', 40),
(5, 'Administrador', 50);

-- --------------------------------------------------------

--
-- Estrutura da tabela `User`
--

CREATE TABLE IF NOT EXISTS `User` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `profile_id` int(10) unsigned NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(50) NOT NULL,
  `enabled` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `email_UNIQUE` (`email`),
  KEY `profile_id_FK` (`profile_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;

--
-- Extraindo dados da tabela `User`
--

INSERT INTO `User` (`id`, `profile_id`, `email`, `password`, `enabled`) VALUES
(1, 5, 'alexsilvamartinsasm@gmail.com', 'e10adc3949ba59abbe56e057f20f883e', 1);

-- --------------------------------------------------------

--
-- Estrutura da tabela `ZipCode`
--

CREATE TABLE IF NOT EXISTS `ZipCode` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `zip_code` varchar(8) NOT NULL,
  `delivery_price` decimal(15,2) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `zip_code_UNIQUE` (`zip_code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

--
-- Constraints for dumped tables
--

--
-- Limitadores para a tabela `Customer`
--
ALTER TABLE `Customer`
  ADD CONSTRAINT `Customer_User_FK` FOREIGN KEY (`user_id`) REFERENCES `User` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `CustomerAddress`
--
ALTER TABLE `CustomerAddress`
  ADD CONSTRAINT `CustomerAddress_Customer_FK` FOREIGN KEY (`customer_id`) REFERENCES `Customer` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `Drink`
--
ALTER TABLE `Drink`
  ADD CONSTRAINT `Drink_DrinkType_FK` FOREIGN KEY (`drink_type_id`) REFERENCES `DrinkType` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `Employer`
--
ALTER TABLE `Employer`
  ADD CONSTRAINT `Employer_User_FK` FOREIGN KEY (`user_id`) REFERENCES `User` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `Order`
--
ALTER TABLE `Order`
  ADD CONSTRAINT `Order_CustomerAddress_FK` FOREIGN KEY (`customer_address_id`) REFERENCES `CustomerAddress` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `Order_PaymentType_FK` FOREIGN KEY (`payment_type_id`) REFERENCES `PaymentType` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `OrderLog`
--
ALTER TABLE `OrderLog`
  ADD CONSTRAINT `OrderLog_OrderStatus_FK` FOREIGN KEY (`order_status_id`) REFERENCES `OrderStatus` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `OrderLog_Order_FK` FOREIGN KEY (`order_id`) REFERENCES `Order` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `OrderLog_User_FK` FOREIGN KEY (`user_id`) REFERENCES `User` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `OrderPizza`
--
ALTER TABLE `OrderPizza`
  ADD CONSTRAINT `OrderPizza_Order_FK` FOREIGN KEY (`order_id`) REFERENCES `Order` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `OrderPizza_Pizza_FK` FOREIGN KEY (`pizza_id`) REFERENCES `Pizza` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `OrdersDrink`
--
ALTER TABLE `OrdersDrink`
  ADD CONSTRAINT `OrdersDrink_Drink_FK` FOREIGN KEY (`drink_id`) REFERENCES `Drink` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `OrdersDrink_Order_FK` FOREIGN KEY (`order_id`) REFERENCES `Order` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `Pizza`
--
ALTER TABLE `Pizza`
  ADD CONSTRAINT `Pizza_PizzaFlavor_FK` FOREIGN KEY (`pizza_flavor_id`) REFERENCES `PizzaFlavor` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `Pizza_PizzaSize_FK` FOREIGN KEY (`pizza_size_id`) REFERENCES `PizzaSize` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `PizzaFlavorIngredient`
--
ALTER TABLE `PizzaFlavorIngredient`
  ADD CONSTRAINT `PizzaFlavorIngredient_Ingredient_FK` FOREIGN KEY (`ingredient_id`) REFERENCES `Ingredient` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `PizzaFlavorIngredient_PizzaFlavor_FK` FOREIGN KEY (`pizza_flavor_id`) REFERENCES `PizzaFlavor` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `User`
--
ALTER TABLE `User`
  ADD CONSTRAINT `User_Profile_FK` FOREIGN KEY (`profile_id`) REFERENCES `Profile` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
