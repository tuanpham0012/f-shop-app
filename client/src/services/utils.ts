import { ref, reactive, computed } from "vue";

// const { t } = i18n.global;
import { useGlobalStore } from "@/stores/global-store";
export const resetError = () => {
  const globalStore = useGlobalStore();
  globalStore.setError({});
};
export const getPortalId = () => {
  return getlocalStorage("portal_global_id");
};
export const setlocalStorage = (key: string, value: any) => {
  localStorage.setItem(key, value);
};
export const getlocalStorage = (key: string) => {
  return localStorage.getItem(key);
};
export const getInnerHeight = () => {
  let minusHeight = 130;
  if (window.innerHeight <= 660) {
    minusHeight = 150;
  }
  return window.innerHeight - minusHeight;
};
export const getMaxHeightScreen = () => {
  const globalStore = useGlobalStore();
  const maxHeightNavbar = computed(() => {
    return globalStore.maxHeightScreen;
  });
  return maxHeightNavbar.value;
};
export const getMaxHeightTalbe = () => {
  return Math.round((getMaxHeightScreen() * 90) / 100);
};

export function removeVietnameseTones(str: string) {
  str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
  str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
  str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
  str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
  str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
  str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
  str = str.replace(/đ/g, "d");
  str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
  str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
  str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
  str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
  str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
  str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
  str = str.replace(/Đ/g, "D");
  // Some system encode vietnamese combining accent as individual utf-8 characters
  // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
  str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
  str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
  // Remove extra spaces
  // Bỏ các khoảng trắng liền nhau
  str = str.replace(/ + /g, " ");
  str = str.trim();
  // Remove punctuations
  // Bỏ dấu câu, kí tự đặc biệt
  str = str.replace(
    /!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g,
    " "
  );
  return str;
}

// export function lang(name: string) {
//   return t(name);
// }

export function alert(init:any, close:any, message: string, color: string) {
  init({ message: message, color: color });
  setInterval(() => {
    close();
  }, 500);
}

export function getAsset(path: string) {
  return new URL(`../` + path, import.meta.url).href;
}

export function listDecimalNumber() {
  const arrays = [2, 3, 4, 5, 6];
  return arrays;
}

export function parseNumber(value: any) {
  if(value === "-"){
    return value;
  }
  if (value == null || value === "") {
    return "";
  }
  const ret = ref();
  if (typeof value === "string") {
    value = value.trim();
    value = value.replace(/,/g, "_"); //Đổi dấu ',' thành dấu _ Biểu hiện dấu thập phân
    value = value.replace(/\./g, "+"); //Đổi dấu '.' thành dấu  + //Biểu hiện phần nguyên
    value = value.replace(/\+/g, "");
    value = value.replace(/_/, ".");
    const check = checkIsFloat(value);
    console.log("Start check is float :");
    console.log(check);
    console.log("End check is float :");
    if (check.status) {
      ret.value = value;
    } else {
      ret.value = parseInt(value);
    }
  }
  console.log("parse int ex " + ret.value);
  return ret.value ? ret.value : 0;
}

export function searchString(num: any, search = "") {
  if (num == 0) {
    return false;
  }
  for (let i = 0; i < num.length; i++) {
    if (num[i] == search) {
      return true;
    }
  }
  return false;
}

export function array_merge(merges = [], datas = []) {
  for (const [key, item] of Object.entries(datas)) {
    merges.push(item);
  }
  return merges;
}

export function flip(array: any, fillValue = null) {
  if (!Array.isArray(array)) {
    throw new Error("flip failed. Input is not an Array");
  }

  const n = array.length;
  const obj:any = {};
  if (fillValue) {
    for (let i = 0; i < n; i++) {
      obj[array[i]] = fillValue;
    }
  } else {
    for (let i = 0; i < n; i++) {
      obj[array[i]] = i;
    }
  }
  return obj;
}

export function textCode(text: string) {
  text = text.replace(/[^\w\s]/gi, "");
  text = text.replace(/\s/g, "");
  text = text.toUpperCase();
  return text;
}

export function toUpperCase(value = "") {
  return value.toUpperCase();
}

export function toTaxCode(num = "", index = 10, item = "-") {
  if (!num) {
    return null;
  }
  let convert = num;
  const newNum = num.toString().replace("-", "").split("");
  if (newNum.length >= 13) {
    newNum.splice(10, 0, item);
    convert = newNum.join("");
    console.log(convert);
  }

  return convert;
}

export function convertOption(entry:any = {}) {
  entry.checked = false;
  entry.show = true;
  entry.label = entry.label || entry.name || "";
  entry.keyword = removeVietnameseTones(entry.label.toLowerCase());
  return entry;
}

export function numberFormat(num:any, decimalLength = 0) {
  num = parseFloat(num);
  return num
    .toFixed(decimalLength) // always two decimal digits
    .replace(".", ",") // replace decimal point character with ,
    .replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1."); // use . as a separator
}

export function displayPrice(number: any) {
  if (number === "" || number === 0) {
    return number;
  }
  return currencyFormatTenant(number);
}

export function currencyFormatTenant(num: any, allowNo = false) {
  if(num === "-"){
    return num;
  }
  if (num == null) {
    return null;
  }
  if (num === 0 && allowNo) {
    return 0;
  }
  if (num == 0) {
    return "";
  }
  const check = checkIsFloat(num);
  if (check.status) {
    if (!check.active) {
      const newValue = ref(num.substr(0, num.length - 1));
      newValue.value = numberFormat(newValue.value, 0);
      return newValue.value + ",";
    }
    return numberFormat(num, check.decimalLength);
  } else {
    return numberFormat(num, 0);
  }
}

export function isFloat(number: any) {
  return Number(number) === number && number % 1 !== 0;
}

function checkIsFloat(num: any) {
  const result = reactive({
    status: true,
    active: false,
    decimalLength: 0,
    number: num,
  });
  if (typeof num === "string") {
    const arrayValue = ref(num.split("."));
    if (arrayValue.value.length == 1) {
      result.status = false;
    } else if (arrayValue.value[1] != "") {
      result.active = true;
      result.decimalLength =
        arrayValue.value[1].toString().length > 2
          ? 2
          : arrayValue.value[1].toString().length;
    }
  } else {
    if (isFloat(num)) {
      const nValue = ref((num + "").split("."));
      const decimal = ref(nValue.value[1].length);
      result.status = true;
      result.active = true;
      result.decimalLength = decimal.value > 2 ? 2 : decimal.value;
    } else {
      result.status = false;
    }
  }

  return result;
}

interface EntryInput {
  [key: string]: any;
}

export function changeEventInput(
  entry: EntryInput,
  fields: string[] = [],
  status = false
) {
  if (fields.length == 0) {
    for (const [key, value] of Object.entries(entry)) {
      entry[key] = status;
    }
  } else {
    for (const [key, value] of Object.entries(fields)) {
      console.log(`${key}: ${value}`);

      entry[value] = status;
    }
  }

  return entry;
}

export function changeEventCheckbox(
  entry: EntryInput,
  fields: string[] = [],
  status: any
) {
  if (fields.length == 0) {
    for (const [key, value] of Object.entries(entry)) {
      entry[key] = status[key] == 0 ? false : true;
    }
  } else {
    for (const [key, value] of Object.entries(fields)) {
      entry[value] = status[key] == 0 ? false : true;
    }
  }

  return entry;
}

export function makeid(length = 6) {
  const result = ref("");
  const characters =
    "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
  const charactersLength = characters.length;
  for (let i = 0; i < length; i++) {
    result.value += characters.charAt(
      Math.floor(Math.random() * charactersLength)
    );
  }
  return result.value;
}

export function isImage(file: any) {
  const arrayType = ["image/png", "image/jpeg", "image/jpg", "image/gif"];
  if (arrayType.indexOf(file.type) != -1) {
    return true;
  }
  return false;
}

type EntryPopup = {
  modal: boolean;
  title_modal: string;
};

export function changePopup(field: EntryPopup, status = false, title = "") {
  field.modal = status;
  field.title_modal = title;
}

export const nName = (firstName: any, lastName = "") => {
  return firstName + " - " + lastName;
};
export const getMinHeight = (height = 350) => {
  return window.innerHeight - height;
};
export const getMaxHeight = (height = 350) => {
  return Math.round((getMinHeight(height) * 90) / 100);
};