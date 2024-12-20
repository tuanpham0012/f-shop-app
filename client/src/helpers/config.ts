export let apiUrl = "";

const env = import.meta.env.VITE_VUE_APP_ENV;
console.log(import.meta.env.VITE_VUE_APP_ENV);

console.log();

switch (env) {
    case "production":
        apiUrl = `https://${import.meta.env.VITE_VUE_APP_DOMAIN_PROD}/api/v1`;
        break;
    case "development":
        apiUrl = `${window.location.protocol}//${window.location.hostname}:8080/api/v1`;
        // apiUrl = 'web-api:8080/api/v1'
        break;
    default:
        apiUrl = `${window.location.protocol}//${window.location.hostname}:5077/api/v1`;
        break;
}
