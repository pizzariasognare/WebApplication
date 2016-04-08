DROP PROCEDURE IF EXISTS `GetTotalOrdersDeliveredToday`;

DELIMITER //
CREATE PROCEDURE GetTotalOrdersDeliveredToday()
BEGIN
    SELECT E.name,
		   OL.order_total,
		   OL.delivery_price_total
	FROM `pizzariasognare.com.br`.`Employer` AS E
	INNER JOIN (SELECT COUNT(OL.id) AS order_total,
					   SUM(O.delivery_price) AS delivery_price_total,
					   OL.user_id
				FROM `pizzariasognare.com.br`.`OrderLog` AS OL
				INNER JOIN `pizzariasognare.com.br`.`Order` AS O ON OL.order_id = O.id
				WHERE OL.order_status_id = 6		
                  AND O.order_date = (DATE((DATE_SUB(NOW(), INTERVAL 2 HOUR))))				
				GROUP BY OL.user_id) AS OL ON OL.user_id = E.user_id;
END //
DELIMITER ;