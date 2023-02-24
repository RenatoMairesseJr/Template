import CryptoJS from "crypto-js"

const criptokey: string = "(H+MbQeThVmYq3t6";//process.env.REACT_APP_CRYPTO_KEY as string
const key = CryptoJS.enc.Utf8.parse(criptokey);
const iv = CryptoJS.enc.Utf8.parse(criptokey);

// Methods for the encrypt and decrypt Using AES
export function protect(text: string) {
    var encrypted = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(text), key, 
    {
        keySize: 128 / 8,
        iv: iv,
        mode: CryptoJS.mode.ECB,
        padding: CryptoJS.pad.Pkcs7
    });

    return encrypted.toString();
}

export function unprotect(decString: string) {
    var decrypted = CryptoJS.AES.decrypt(decString, key, {
        keySize: 128 / 8,
        iv: iv,
        mode: CryptoJS.mode.CBC,
        padding: CryptoJS.pad.Pkcs7
    });
    return decrypted.toString(CryptoJS.enc.Utf8)
}
