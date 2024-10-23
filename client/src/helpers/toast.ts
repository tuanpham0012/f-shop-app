import Swal from "sweetalert2";
const Toast = Swal.mixin({
  toast: true,
  position: "top-end",
  showConfirmButton: false,
  timer: 2000,
  timerProgressBar: true,
  didOpen: (toast) => {
    toast.onmouseenter = Swal.stopTimer;
    toast.onmouseleave = Swal.resumeTimer;
  },
});

export const successMessage = (message: string) =>
  Toast.fire({
    icon: "success",
    title: message,
  });

export const errorMessage = (message: string) =>
  Toast.fire({
    icon: "error",
    title: message,
  });

export const confirmAlert = ({
  title = "Are you sure?",
  text = "You won't be able to revert this!",
  confirmButtonColor = "#3085d6",
  cancelButtonColor = "#d33",
  confirmButtonText = "Yes, delete it!",
  cancelButtonText= "No, cancel!",
}) => {
  return Swal.fire({
    title: title,
    text: text,
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: confirmButtonColor,
    cancelButtonColor: cancelButtonColor,
    confirmButtonText: confirmButtonText,
    cancelButtonText: cancelButtonText
  });
};
