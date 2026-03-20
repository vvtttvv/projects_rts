## Task I — Service Locator

### What is a Service Locator?
A **Service Locator** is a pattern that acts like a "storage" for your application's services. Instead of creating objects manually everywhere in your code, you **register** them once and **get** them whenever you need.

---

### Your Goal
Build a `ServiceLocator` class in C# that can:

- **Register** a service — store it by its interface type
- **Get** a service — retrieve it by its interface type
- **Singleton mode** — always return the same instance
- **Transient mode** — return a new instance every time
- **Resolve dependencies** — if service A needs service B, it should be resolved automatically

---

### Rules
Your implementation must handle these error cases:

| Situation | What should happen |
|---|---|
| A service has more than 1 constructor | Throw an exception |
| The same service is registered twice | Throw an exception |
| You try to get a service that was never registered | Throw an exception |

---

### How do you know it works?
Write tests that verify:

1. Two calls to `Get<T>()` in **transient** mode return **different** objects
2. Two calls to `Get<T>()` in **singleton** mode return the **same** object
3. A service with a dependency on another service is resolved correctly
4. Registering the same service twice causes an error
5. Registering a service with multiple constructors causes an error

---

### Hint
Think of it like a dictionary — the **key** is the interface type, the **value** is a function that creates the service.