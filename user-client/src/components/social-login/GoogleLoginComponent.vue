<template>
    <!-- <GoogleLogin :callback="callback" class="hidden">
        <a href="#" class="btn btn-login btn-g">
            <i class="icon-google"></i>
            Login With Google
        </a>
      </GoogleLogin
    > -->
    <GoogleLogin style="display: unset" :callback="callback" flow="implicit" ux_mode="redirect" prompt auto-login>
        <button class="btn btn-login btn-g w-100"><i class="icon-google"></i> Login With Google</button>
    </GoogleLogin>
</template>

<script setup>
import { onMounted } from "vue";
import { useAuthStore } from "@/stores/auth";

// const login = () => {
//     googleAuthCodeLogin().then(async (response) => {
//         console.log("Handle the response", response);
//         console.log("Access Token", JSON.stringify(response.credential));
//         await authStore
//             .loginWithGoogle(response.code)
//             .then((res) => {
//                 // Handle successful login
//                 console.log("Login successful");
//                 console.log("User Info", res.data);
//             })
//             .catch((error) => {
//                 // Handle error
//                 console.error("Login failed", error);
//             });
//     });
// };

const authStore = useAuthStore();

const callback = async (response) => {
  // This callback will be triggered when the user selects or login to
  // his Google account from the popup
  console.log("Đã nhận được response từ Google:", response);
  let token = '';
  let type = 1;
  if (response.credential) {
    token = response.credential;
    type = 2;
  }else if (response.code) {
    token = response.code;
    type = 1;
  } else {
    console.error("Không nhận được mã xác thực từ Google");
    return;
  }

  await authStore.loginWithGoogle(token, type);

};
onMounted(() => {

})
</script>
