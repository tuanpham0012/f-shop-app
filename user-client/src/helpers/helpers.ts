import { apiUrl } from "@/helpers/config";

export const printfValueArray = (arr: any) => {
    let result = "";
    for (let i = 0; i < arr.length; i++) {
        result += arr[i] + "\n";
    }
    return result;
};

export const randomPassword = (length = 8, symbol = true, lowercase = true) => {
    var c = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var charset =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    charset = lowercase ? charset + 'abcdefghijklmnopqrstuvwxyz' : charset
    var punctuation = symbol ? '!@#$%&' : '';
    var retVal = c.charAt(Math.floor(Math.random() * c.length));
    for (var i = 0; i < length - 2; ++i) {
        retVal += charset.charAt(Math.floor(Math.random() * charset.length));
    }
    return (
        retVal +
        punctuation.charAt(Math.floor(Math.random() * punctuation.length))
    );
};
export const isNumber = (evt:any) => {
    evt = evt ? evt : window.Event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        evt.preventDefault();
        return false;
    }
    return true;
};

export const viewFile = (link:string, def:any = null) => {
    if(!link)
        return def ?? 'https://img.freepik.com/premium-vector/default-image-icon-vector-missing-picture-page-website-design-mobile-app-no-photo-available_87543-11093.jpg'
    if(link && link.length < 250){
        return `${apiUrl}/files/download/${link}`
    }
    return link
}

export function textCode(text: string) {
    text = text.replace(/[^\w\s]/gi, "");
    text = text.replace(/\s/g, "");
    text = text.toUpperCase();
    return text;
  }
