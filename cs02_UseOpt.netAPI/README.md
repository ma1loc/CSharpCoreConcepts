first step form the back end durring the enable the 'enable tow factor authentication' button the front send an api call to the back end.

the backend take the call and at first step is generate a 20 random char, based on the TOPT standerd, that happend based on the methode comming from the 'Otp.net' packge.

that mean's most calling the KeyGeneration from Otp.NET package.

i will expline what i understand form this step:

// using the package
using OtpNet;

// creating an instance of the Totp object class but i have a question the param that's taking is it comming from my .env as SECRETE_KEY that make the keystored, but the .env key used here for what exatly?
var totp = new Totp(secretKey);

// change the behaver to another hashing function, defult sha1
var totp = new Totp(secretKey, mode: OtpHashMode.Sha512);


// a new code will be generated every 15 seconds
var totp = new Totp(secretKey, step: 15);

// digit size, that will be apeerase each 15sec
var totp = new Totp(secretKey, totpSize: 8);

// setup the defult time the TOPT will be based on, and it's a standerd time, in all the system in the world based on
var totpCode = totp.ComputeTotp(); // by defualt used the UTC time???

// i have no idea bout this checked what exactly no idea, at all, even is that check will be in the POST /api/auth/2fa/setup end-point api
public bool VerifyTotp(string totp, out long timeWindowUsed, VerificationWindow window = null);
public bool VerifyTotp(DateTime timestamp, string totp, out long timeWindowUsed, VerificationWindow window = null)
