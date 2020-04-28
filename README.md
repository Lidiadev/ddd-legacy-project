Domain-Driven Design - sample to showcase how to work with legacy projects

### Project Details

The sample project is a **Package Delivery System** which keeps track of packages for delivery with the following features:
- create delivery: select customer and address
- edit package: select products
- send package to customer.

The Package Delivery System is a desktop application written in WPF. It uses Dapper to execute SQL queries.

### The Anti-Corruption Layer 
The anti-corruption layer is an adapter layer which handles the conversation between the legacy code and the bubble context.

Considerations:
- the anti-corruption layer is an additional service that must be managed, maintained and scaled
- it may add latency.

### How to refactor

- introduce a bubble context with the new domain model with proper encapsulation	
- create the anti-corruption layer to handle the conversation between the legacy code and the bubble context (through repositories in this case)	

<img alt="anticorruption layer" src="https://github.com/Lidiadev/ddd-legacy-project/blob/master/images/anticorruption_layer.PNG" width="55%" height="55%">

- promote the anti-corruption layer to the synchronizing anticorruption layer which resides in its own bounded context (create a separate DB for the bubble context)

<img alt="sync layer" src="https://github.com/Lidiadev/ddd-legacy-project/blob/master/images/sync_layer.PNG" width="70%" height="70%">

For the current sample, the following approach has been used:
- created triggers for both databases to monitor changes
- only monitored the data the context own
- added a new table synchronization in order to reduce the pressure on the domain tables
- the synchronization between the bubble context DB and the legacy DB is done asynchronously 
- the bubble has been decoupled from ACL.

### Branches

- [Initial](https://github.com/Lidiadev/ddd-legacy-project/tree/intial) - the `initial` branch contains the legacy project.
- Master - the `master` branch will contain the code after refactoring. 
