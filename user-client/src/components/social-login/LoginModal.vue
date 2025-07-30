<template lang="">
    <div class="modal fade show" style="display: block; padding-right: 15px" aria-modal="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="auth-modal">
                    <button class="close-btn" @click="() => emits('close')">×</button>

                    <div class="tabs">
                        <button class="tab-link" :class="{ 'active' : tabId == 0}" @click="tabId = 0">Sign In</button>
                        <button class="tab-link" :class="{ 'active' : tabId == 1}" @click="tabId = 1">Register</button>
                    </div>

                    <div class="tab-content">
                        <!-- Sign In Form -->
                        <div id="signin" class="form-container active">
                            <form>
                                <div class="form-group">
                                    <label for="signin-username">Username or email address *</label>
                                    <input type="text" id="signin-username" required />
                                    <!-- Thêm thẻ này để hiển thị thông báo lỗi như trong ảnh -->
                                    <!-- <div class="validation-message">Please fill out this field.</div> -->
                                </div>
                                <div class="form-group">
                                    <label for="signin-password">Password *</label>
                                    <input type="password" id="signin-password" required />
                                </div>
                                <div class="form-row">
                                    <button type="submit" class="btn-submit">LOG IN <span class="arrow">→</span></button>
                                    <div class="checkbox-group">
                                        <input type="checkbox" id="remember" />
                                        <label for="remember">Remember Me</label>
                                    </div>
                                    <a href="#" class="forgot-password">Forgot Your Password?</a>
                                </div>
                            </form>
                        </div>

                        <!-- Register Form -->
                        <div id="register" class="form-container">
                            <form>
                                <div class="form-group">
                                    <label for="register-email">Your email address *</label>
                                    <input type="email" id="register-email" required />
                                </div>
                                <div class="form-group">
                                    <label for="register-password">Password *</label>
                                    <input type="password" id="register-password" required />
                                </div>
                                <div class="form-row">
                                    <button type="submit" class="btn-submit">SIGN UP <span class="arrow">→</span></button>
                                    <div class="checkbox-group">
                                        <input type="checkbox" id="agree" required />
                                        <label for="agree">I agree to the <a href="#">privacy policy</a> *</label>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>

                    <!-- Common Social Login Section -->
                    <div class="separator">or sign in with</div>
                    <div class="social-logins">
                        <GoogleLoginComponent></GoogleLoginComponent>
                        <button class="social-btn facebook">
                            <img src="https://upload.wikimedia.org/wikipedia/commons/b/b8/2021_Facebook_icon.svg" alt="Facebook logo" />
                            Login With Facebook
                        </button>
                    </div>
                </div>
            </div>
            <!-- End .modal-content -->
        </div>
        <!-- End .modal-dialog -->
    </div>
</template>
<script setup lang="ts">
import { onBeforeMount, onBeforeUnmount, onMounted, ref, computed, watch } from "vue";
import GoogleLoginComponent from "./GoogleLoginComponent.vue";
import { useAuthStore } from "@/stores/auth";

const body = document.body;

const emits = defineEmits(["close"]);

let savedScrollY = ref(0); // Biến để lưu vị trí scroll

const authStore = useAuthStore();

const tabId = ref(0);

const isLoggedIn = computed(() => authStore.$state.info ?? false);

watch(
    () => authStore.$state.info,
    (newInfo) => {
        if (newInfo !== null) {
            emits("close");
        } else {
            console.log("User is logged out");
        }
    }
);

onBeforeMount(() => {
    savedScrollY.value = window.scrollY;

    // Áp dụng style để "đóng băng" body
    body.style.position = "fixed";
    body.style.top = `-${savedScrollY}px`;
    body.style.width = "100%";
});

onMounted(() => {
    // Mở modal khi component được mount
    document.addEventListener("DOMContentLoaded", function () {
        const tabLinks = document.querySelectorAll(".tab-link");
        const formContainers = document.querySelectorAll(".form-container");

        tabLinks.forEach((link) => {
            link.addEventListener("click", () => {
                const tabId = link.getAttribute("data-tab");

                // Remove active class from all tabs and forms
                tabLinks.forEach((item) => item.classList.remove("active"));
                formContainers.forEach((item) => item.classList.remove("active"));

                // Add active class to clicked tab and corresponding form
                link.classList.add("active");
                if (tabId) {
                    const tabElement = document.getElementById(tabId);
                    if (tabElement) {
                        tabElement.classList.add("active");
                    }
                }
            });
        });
    });
});

onBeforeUnmount(() => {
    body.style.position = "";
    body.style.top = "";
    body.style.width = "";

    // Trả lại vị trí scroll cũ
    window.scrollTo(0, savedScrollY.value);
});
</script>
<style lang="scss" scoped>
@import url("https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600&display=swap");

:root {
    --bs-primary: #c77833;
    --border-color: #e0e0e0;
    --text-color: #333;
}

body {
    font-family: "Poppins", sans-serif;
    background-color: #f4f4f4;
    display: flex;
    justify-content: center;
    align-items: center;
    min-height: 100vh;
    margin: 0;
    color: #333;
}

/* --- Modal Container --- */
.auth-modal {
    background-color: #fff;
    padding: 40px;
    border-radius: 8px;
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
    width: 100%;
    position: relative;
}

.close-btn {
    position: absolute;
    top: 15px;
    right: 20px;
    background: none;
    border: none;
    font-size: 24px;
    color: #333;
    cursor: pointer;
}

/* --- Tabs --- */
.tabs {
    display: flex;
    border-bottom: 1px solid #e0e0e0;
    margin-bottom: 30px;
}

.tab-link {
    flex: 1;
    padding: 15px;
    text-align: center;
    font-size: 22px;
    font-weight: 500;
    cursor: pointer;
    background: none;
    border: none;
    color: #333;
    position: relative;
    transition: color 0.3s ease;
}

.tab-link.active {
    color: #333;
}

/* Orange underline for active tab */
.tab-link.active::after {
    content: "";
    position: absolute;
    bottom: -1px; /* Place it on top of the container's border */
    left: 0;
    right: 0;
    height: 2px;
    background-color: var(--bs-primary);
}

/* --- Form Content --- */
.form-container {
    display: none; /* Hide forms by default */
}

.form-container.active {
    display: block; /* Show active form */
}

.form-group {
    margin-bottom: 20px;
}

.form-group label {
    display: block;
    margin-bottom: 8px;
    font-size: 14px;
    color: #333;
}

.form-group input[type="text"],
.form-group input[type="email"],
.form-group input[type="password"] {
    width: 100%;
    padding: 12px;
    border: 1px solid #e0e0e0;
    border-radius: 4px;
    font-size: 16px;
    box-sizing: border-box; /* Important for padding and width */
}

.form-group input:focus {
    outline: none;
    border-color: var(--bs-primary);
}

/* Example validation message from image */
.validation-message {
    border: 1px solid var(--bs-primary);
    padding: 5px 8px;
    font-size: 12px;
    background-color: #fff;
    position: relative;
    top: -30px; /* Adjust as needed */
    left: 10px;
    display: inline-block;
    z-index: 1;
}

/* --- Form Actions & Links --- */
.form-row {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 30px;
}

.checkbox-group {
    display: flex;
    align-items: center;
    font-size: 14px;
    color: #333;
}

.checkbox-group input[type="checkbox"] {
    margin-right: 8px;
    accent-color: var(--bs-primary);
}

.checkbox-group a {
    color: var(--bs-primary);
    text-decoration: none;
    font-weight: 500;
}

.forgot-password {
    font-size: 14px;
    color: #333;
    text-decoration: none;
}

.forgot-password:hover,
.checkbox-group a:hover {
    text-decoration: underline;
}

/* --- Buttons --- */
.btn-submit {
    padding: 12px 25px;
    background-color: #fff;
    border: 1px solid var(--bs-primary);
    color: var(--bs-primary);
    font-size: 14px;
    border-radius: 4px;
    cursor: pointer;
    transition: background-color 0.3s, color 0.3s;
}

.btn-submit:hover {
    background-color: var(--bs-primary);
    color: #fff;
    font-weight: 600;
}

/* --- Separator "or sign in with" --- */
.separator {
    display: flex;
    align-items: center;
    text-align: center;
    color: #333;
    margin: 30px 0;
    font-size: 14px;
}

.separator::before,
.separator::after {
    content: "";
    flex: 1;
    border-bottom: 1px solid #e0e0e0;
}

.separator:not(:empty)::before {
    margin-right: 0.5em;
}

.separator:not(:empty)::after {
    margin-left: 0.5em;
}

/* --- Social Login Buttons --- */
.social-logins {
    display: flex;
    gap: 15px;
}

.social-btn {
    flex: 1;
    display: flex;
    align-items: center;
    justify-content: center;
    padding: 12px;
    border: 1px solid #e0e0e0;
    border-radius: 4px;
    background-color: #fff;
    font-size: 14px;
    cursor: pointer;
    transition: background-color 0.3s, border-color 0.3s;
}

.social-btn:hover {
    border-color: #aaa;
}

.social-btn img {
    width: 20px;
    height: 20px;
    margin-right: 10px;
}
</style>
