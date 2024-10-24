export let apiUrl = "";

const env = import.meta.env.VITE_VUE_APP_ENV;
console.log(import.meta.env.VITE_VUE_APP_ENV);

console.log(window.location.hostname);



switch (env) {
    case "production":
        apiUrl = `https://${import.meta.env.VITE_VUE_APP_DOMAIN_PROD}/api/v1`;
        break;
    case "development":
        apiUrl = `${window.location.hostname}:${import.meta.env.VITE_VUE_APP_PORT_DEV}/api/v1`;
        break;
    default:
        apiUrl = "http://localhost:5077/api/v1";
        break;
}
