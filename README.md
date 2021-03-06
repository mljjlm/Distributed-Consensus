# Bachelor Thesis - Consensus of History in Distributed DCR Graphs
The repository for our bachelor project concerning creating a valid history of events in a distributed system.

## Project Definition
_Given an execution of a distributed DCR graph, the purpose of this project is to find an algorithm which uses the local orders of execution of the individual events to find a global order of execution with the least amount of concurrency, where the local orders are preserved in the global order. Furthermore, the algorithm should be able to observe and if possible, identify which events in the workflow are part of malicious behaviour, where malicious behaviour is the act of disobeying the rules of the workflow or reporting false information. Finally the algorithm should establish distributed consensus on the order of execution among the events._

### Project Introduction

Dynamic Condition Response Graphs (DCR graphs) are developed principally by Thomas Hildebrandt at the IT University of Copenhagen, in collaboration with ResultMaker and later Exformatics A/S. DCR graphs are used to model workflows which represents work processes.

DCR graphs are used by companies to ensure that business procedures are executed according to business rules. At a given time a business might want to see in which order events in a given DCR graph have executed, a so called _order of execution_. The order of execution can contain interesting information for the business, as it reveals which events have been executed to lead to the current state of the workflow. Furthermore, if a DCR graph is used to represent a case of a customer, and if multiple case workers are working on that case, and do not have a total overview of it, an order of execution can provide that overview.

Since DCR graphs allow infinite behaviour, the workflow can be in the same state multiple times. This leaves the case worker guessing how many times the workflow has been in the current state. Finally, for DCR graphs shared between two or more companies, one of the participating companies might want to check retroactively whether the other companies have followed the rules of the workflow.

The events of a DCR graph can be distributed on a number of processes over a network allowing better scalability, reliability as well as allowing multiple companies to work together, by hosting and handling different events themselves.

In a distributed DCR graph finding an order of execution can be difficult, because no single pro- cess has an overview over the entire workflow. Information about what has happened can be split among several processes, the clocks of processes are not necessarily synchronised, and processes may act maliciously, that is they may not follow the rules of the workflow or they may emit erroneous information. Even though there are challenges, DCR graphs provide information that can be used to relate executions with one another, which is especially helpful when information of two or more events need to be combined into an order of execution.

Any malicious activity should be observable and if possible the events that are behind this activity should be identified. Handling message tampering, that is altering, replaying and with- holding messages before sending them to the intended recipient as described in and processes crashing while carrying out a task, are out of scope for this project.

By researching and applying distributed system theories and algorithms, as well as the rules of DCR graphs, we will try to solve the problem of generating and reaching consensus of histories of DCR graphs.
