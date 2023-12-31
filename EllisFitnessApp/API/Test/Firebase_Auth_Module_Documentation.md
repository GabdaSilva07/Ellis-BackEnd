# Firebase Authentication Module Documentation

## Overview

This module is designed to handle user authentication through Firebase. It includes methods for signing in, fetching the authentication token, and initializing the Firebase SDK with environment variables.

## Table of Contents

- [Getting Started](#getting-started)
- [Dependencies](#dependencies)
- [Environment Variables](#environment-variables)
- [Key Components](#key-components)
- [Code Example](#code-example)
- [How to Use](#how-to-use)
- [Error Handling](#error-handling)
- [Contributing](#contributing)

---

## Getting Started

To get the project running, clone the repository and run:
\```bash
npm install
\```

## Dependencies

- `firebase/app`
- `firebase/auth`
- `dotenv`

Make sure to install these dependencies before running the module.

## Environment Variables

Create a `.env` file at the root of your project and add the following keys:
\```env
FIREBASE_API_KEY=Your_Firebase_API_Key_Here
FIREBASE_AUTH_DOMAIN=Your_Firebase_Auth_Domain_Here
\```
These values are used to initialize Firebase and should be kept secure.

## Key Components

### Initialize Firebase

Firebase is initialized using the `initializeApp()` function from `firebase/app`. The API key and Auth Domain are fetched from environment variables.

### User Authentication

User authentication is done using `signInWithEmailAndPassword()` from `firebase/auth`.

### Fetching User Token

A user token is fetched post successful sign-in using `getIdToken()` method.

## Code Example

Below is an example code snippet demonstrating how to use the Firebase Authentication Module:

\```javascript
import { initializeApp } from "firebase/app";
import { getAuth, signInWithEmailAndPassword } from "firebase/auth";
import { config } from "dotenv";

config();

// Initialize Firebase with your config
const firebaseConfig = {
apiKey: process.env.FIREBASE_API_KEY,
authDomain: process.env.FIREBASE_AUTH_DOMAIN,
};

initializeApp(firebaseConfig);
const auth = getAuth();

// Test user credentials
const email = "testuser@gmain.com";
const password = "155155Gs";

// Sign in and get the ID token
async function signInAndGetToken() {
try {
const userCredential = await signInWithEmailAndPassword(
auth,
email,
password
);
const user = userCredential.user;
const idToken = await user.getIdToken();
console.log("ID Token", idToken);
} catch (error) {
console.log("Error:", error);
}
}

signInAndGetToken();
\```

## How to Use

Run the script using:
\```bash
node your_script_name_here.js
\```
The ID Token will be printed in the console post successful authentication.

## Error Handling

The module uses try-catch blocks to handle errors. Any error will be logged to the console for further diagnosis.

## Contributing

For contributions, please create a new branch and submit a pull request for review.
