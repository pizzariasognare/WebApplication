CREATE TRIGGER `pizzariasognare.com.br`.`Order_AFTER_UPDATE` AFTER UPDATE ON `Order` FOR EACH ROW
BEGIN
	UPDATE `pizzariasognare.com.br`.`Order` SET NEW.final_price = ((NEW.final_price + NEW.price + NEW.delivery_price) - NEW.discount) WHERE id = NEW.id;
END