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
//const email = "testuser1@gmail.com";
const email = "testuser@gmain.com";
const password = "155155Gs";

// Sign in and get the ID token
async function signInAndGetToken() {
  try {
    const userCredential = await signInWithEmailAndPassword(
      auth,
      email,
      password,
    );
    const user = userCredential.user;
    const idToken = await user.getIdToken();
    console.log("ID Token", idToken);
  } catch (error) {
    console.log("Error:", error);
  }
}

signInAndGetToken();
