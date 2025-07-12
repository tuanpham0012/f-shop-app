<template>
  <GoogleLogin :callback="callback" />
</template>

<script setup>
import { GoogleLogin } from 'vue3-google-login'
import { useAuthStore } from '@/stores/auth'

const authStore = useAuthStore()

const callback = async (response) => {
  // This callback will be triggered when the user selects or login to
  // his Google account from the popup
  console.log("Handle the response", response)
  console.log("Access Token", JSON.stringify(response.credential ))
  await authStore.loginWithGoogle(response.credential).then((res) => {
    // Handle successful login
    console.log("Login successful")
    console.log("User Info", res.data)
  }).catch((error) => {
    // Handle error
    console.error("Login failed", error)
  })
}
</script>