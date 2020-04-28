Domain-Driven Design - sample to showcase how to work with legacy projects

### Project Details

The sample project is a **Package Delivery System** which keeps track of packages for delivery with the following features:
- create delivery: select customer and address
- edit package: select products
- send package to customer.

The Package Delivery System is a desktop application written in WPF. It uses Dapper to execute SQL queries.

### How to refactor

- introduce a bubble context with the new domain model with proper encapsulation	
- create the anticorruption layer to handle the conversation between the legacy code and the bubble context (through repositories in this case)	

<img alt="anticorruption layer" src="https://github.com/Lidiadev/ddd-legacy-project/blob/master/images/anticorruption_layer.PNG" width="55%" height="55%">

- promote the anticorruption layer to the synchronizing anticorruption layer which resides in its own bounded context

<img alt="sync layer" src="https://github.com/Lidiadev/ddd-legacy-project/blob/master/images/sync_layer.PNG" width="70%" height="70%">

### Branches

- [Initial](https://github.com/Lidiadev/ddd-legacy-project/tree/intial) - the `initial` branch contains the legacy project.
- Master - the `master` branch will contain the code after refactoring. 
