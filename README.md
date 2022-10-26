# Tire Way
A Browser Dealership software designed to handle customer tire orders.

<img src="https://user-images.githubusercontent.com/80795010/198119248-ce22b5d1-e30f-4da5-9fca-eb9f60889a49.gif" alt="" style="max-width: 100%; display: inline-block;" data-target="animated-image.originalImage"> </img>

# Description
Tire Way allows the counterperson to add tires to a customer's order. Each customer comes with a unique invoice, and each invoice is saved to the database so the orders can never get deleted unless requested.

- Home: The invoice page displays only when a counterperson has selected or added a customer. It presents all of the tires that have been added, including info such as the tires part #, price, quantity, etc. Customer information is also displayed on the top left.

- Customers: This page consists of both forms for searching a customer that already exist or creating a customer.

- Tires: The tires section reveals all tires that were added to stock in the database. A counterperson can select a quantity and add it to a customer invoice. If tires are removed from an invoice, they will be added back to stock.

- Stock: This screen allows the counterperson to add tires to stock.

# Custom Commands
- L#Q#: The # after the "L" represents the line number you want to change. The # after the "Q" represents the new quantity of the selected tires.
- 11: Removes all tires off an invoice.
