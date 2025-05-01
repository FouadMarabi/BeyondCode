# SOLID Principles in Software Design

The **SOLID** principles are a set of five design guidelines that help software developers build systems that are easy to understand, flexible, and maintainable. These principles are especially valuable in object-oriented design but apply broadly to software architecture.

---

## 🧱 What is SOLID?

**SOLID** is an acronym for five principles:

- **S** – Single Responsibility Principle (SRP)  
- **O** – Open/Closed Principle (OCP)  
- **L** – Liskov Substitution Principle (LSP)  
- **I** – Interface Segregation Principle (ISP)  
- **D** – Dependency Inversion Principle (DIP)

Together, they promote **clean code**, **low coupling**, and **high cohesion**, making your code easier to extend, refactor, and test.

---

## 🚀 Why Use SOLID Principles?

✅ Encourages better **modularization**  
✅ Improves **code readability and reusability**  
✅ Makes systems **easier to test**  
✅ Helps in applying **dependency injection**  
✅ Reduces the cost of **maintaining and extending** code  
✅ Prevents **rigid**, **fragile**, and **immobile** designs

---

## 📘 The Principles

### 1. Single Responsibility Principle (SRP)

> A class should have **only one reason to change**.

- Each class should focus on a **single task or responsibility**.
- Keeps code **focused** and **easier to test**.

✅ Good: A `UserService` handles user-related logic only.  
🚫 Bad: A `UserService` that also logs, saves files, and sends emails.

**Example:** [View SRP Example](./1_SingleResponsibilityPrinciple_SRP/SRP.cs)
---

### 2. Open/Closed Principle (OCP)

> Software entities should be **open for extension**, but **closed for modification**.

- You should be able to add new behavior **without changing** existing code.
- Often implemented using **abstraction** and **polymorphism**.

✅ Good: Use interfaces and inheritance to add features.  
🚫 Bad: Modify core logic every time a new case is added.

**Example:** [View OCP Example](./2_Open_Closed_Principle_OCP/OCP.cs)
---

### 3. Liskov Substitution Principle (LSP)

> Objects of a superclass should be **replaceable with objects of a subclass** without altering the correctness of the program.

- Subclasses should behave in ways that **do not violate** the expectations of the base class.
- It ensures **correct inheritance**.

✅ Good: `Bird` → `Parrot`, both can `Fly()`  
🚫 Bad: `Bird` → `Penguin` that throws `NotImplementedException` on `Fly()`

**Example:** [View LSP Example](./3_Liskov_Substitution_Principle_LSP/LSP.cs)
---

### 4. Interface Segregation Principle (ISP)

> Clients should not be forced to depend on **interfaces they do not use**.

- Prefer **small, specific interfaces** over large, general ones.
- Prevents classes from implementing unnecessary methods.

✅ Good: `IPrintable`, `IScannable`, `IFaxable`  
🚫 Bad: One huge `IMultifunctionDevice` interface for all
**Example:** [View ISP Example](./4_Interface_Segregation_Principle_ISP/ISP.cs)

---

### 5. Dependency Inversion Principle (DIP)

> High-level modules should not depend on low-level modules. **Both should depend on abstractions**.

- Use **interfaces or abstract classes** so that implementations can be swapped.
- Encourages **dependency injection**.

✅ Good: A service depends on an `ILogger` interface  
🚫 Bad: A service creates and uses a `ConsoleLogger` directly
**Example:** [View DIP Example](./5_Dependency_Inversion_Principle_DIP/DIP.cs)

---

## 🛠️ Applying SOLID in Practice

- Start with a clear domain model.
- Use interfaces to separate behaviors.
- Inject dependencies via constructors.
- Continuously refactor to improve separation and cohesion.
- Test behaviors in isolation.

---

## 📚 Further Reading

- _Clean Code_ by Robert C. Martin  
- _Clean Architecture_ by Robert C. Martin  
- SOLID Principle Series on [Your Favorite Blog or Video Series]  

---

## ❤️ Summary

The SOLID principles are **guiding philosophies**, not rigid rules. Apply them thoughtfully to write code that's not only functional, but also maintainable, flexible, and fun to work with.

