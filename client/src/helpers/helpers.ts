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

export const viewFile = (link:string) => {
    if(!link)
        return 'https://img.freepik.com/premium-vector/default-image-icon-vector-missing-picture-page-website-design-mobile-app-no-photo-available_87543-11093.jpg'
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

export const createSlug = (text: string) => {
    // Chuyển sang chữ thường
    let slug = text.toLowerCase();

    // Thay thế các ký tự đặc biệt
    slug = slug.normalize('NFD').replace(/[\u0300-\u036f]/g, ''); // Loại bỏ dấu
    slug = slug.replace(/[^a-z0-9\s-]/g, ''); // Loại bỏ ký tự không hợp lệ

    // Thay thế khoảng trắng và các ký tự liên tiếp bằng dấu gạch ngang
    slug = slug.trim().replace(/\s+/g, '-'); // Thay thế khoảng trắng thành dấu gạch ngang
    slug = slug.replace(/--+/g, '-'); // Loại bỏ các dấu gạch ngang liên tiếp

    return slug;
}

export const resizeImage = (
    imageSrc: any,
    type: any,
    targetWidth: any = null,
    targetHeight: any = null,
    maxSize = 500
  ) => {
    return new Promise((resolve, reject) => {
      const img = new Image();
      img.crossOrigin = "anonymous";
      img.onload = () => {
        try {
          const canvas = document.createElement("canvas");
          const ctx: any = canvas.getContext("2d");
  
          console.log(`i-w: ${img.width}, i-h: ${img.height}`);

          if (targetWidth == null || targetHeight == null) {
            targetWidth = img.width;
            targetHeight = img.height;
  
            if (img.height > img.width) {
              targetHeight = maxSize;
              targetWidth = targetWidth / (img.height / targetHeight);
            } else {
              targetWidth = maxSize;
              targetHeight = targetHeight /(img.width / targetWidth);
            }
          }
          console.log(`w: ${targetWidth}, h: ${targetHeight}`);
          
          // Set kích thước của canvas
          canvas.width = targetWidth;
          canvas.height = targetHeight;
  
          // Vẽ ảnh lên canvas và resize
          ctx.drawImage(img, 0, 0, targetWidth, targetHeight);
          
          // Chuyển canvas thành base64
          resolve(canvas.toDataURL(type));
        } catch (err) {
          reject(err);
        }
      };
      img.onerror = (error) => {
        reject(error); // Xử lý lỗi nếu không load được ảnh
      };
  
      img.src = imageSrc;
    });
  };
