import axios from "axios";
import {successMessage, errorMessage} from './toast'

const headers = {
    'Content-Type': 'application/json',
    'Accept': 'application/json',
}
const axiosDefaults = {
    headers
}
const http = axios.create(axiosDefaults)
    //register interceptor like this
http.interceptors.request.use(
    (response) => {
        // if(response.headers.loading){
        //     document.body.classList.add("loading");
        // }else{
        //     document.body.classList.remove("loading");
        // }
        return response;
    },
    (error) => {
        console.log('err Data');
        return Promise.reject(error);
    }
)

http.interceptors.response.use(response => {
    // console.log("success: ", response);
    document.body.classList.remove("loading");
    return response;
}, error => {
    console.log("error:", error);
    errorMessage(error.message ?? 'something went wrong!')
    document.body.classList.remove("loading");
    return Promise.reject(error);
});

export const _getList = async (url:any, params:any, header:any = {}) => {
    return await http({
        url: url,
        method: "GET",
        params: params,
        headers: {
            // ...this.headers,
            loading: true,
            ...header,
        }
    });
}

export const _create = (url:any, data:any, header:any = {}) => {
    return http({
        url: url,
        method: "POST",
        data: data,
        headers: {
            // ...this.headers,
            loading: false,
            ...header,
        }
    });
}

export const _show = async(url:any, params:any = {}, header:any = {}) => {
    return await http({
        url: url,
        method: "GET",
        params: params,
        headers: {
            // ...this.headers,
            loading: false,
            ...header,
        }
    });
}

export const _update = (url:any, data:any, header:any = {}) => {
    return http({
        url: url,
        method: "PUT",
        data: data,
        headers: {
            // ...this.headers,
            loading: false,
            ...header,
        }
    });
}

export const _destroy = (url:any, header:any = {}) => {
    return http({
        url: url,
        method: "DELETE",
        headers: {
            // ...this.headers,
            loading: false,
            ...header,
        }
    });
}
