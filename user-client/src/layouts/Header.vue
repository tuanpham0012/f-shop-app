<template>
  <header class="header" id="header" ref="header">
    <div class="header-top" v-if="false">
      <div class="container-lg">
        <div class="header-left">
          <div class="header-dropdown">
            <a href="#">Usd</a>
            <div class="header-menu">
              <ul>
                <li><a href="#">Eur</a></li>
                <li><a href="#">Usd</a></li>
              </ul>
            </div>
            <!-- End .header-menu -->
          </div>
          <!-- End .header-dropdown -->

          <div class="header-dropdown">
            <a href="#">Eng</a>
            <div class="header-menu">
              <ul>
                <li><a href="#">English</a></li>
                <li><a href="#">French</a></li>
                <li><a href="#">Spanish</a></li>
              </ul>
            </div>
            <!-- End .header-menu -->
          </div>
          <!-- End .header-dropdown -->
        </div>
        <!-- End .header-left -->

        <div class="header-right">
          <ul class="top-menu">
            <li>
              <a href="#">Links</a>
              <ul>
                <li>
                  <a href="tel:#"
                    ><i class="icon-phone"></i>Call: +0123 456 789</a
                  >
                </li>
                <li><a href="about.html">About Us</a></li>
                <li><a href="contact.html">Contact Us</a></li>
                <li>
                  <a href="#signin-modal" data-toggle="modal"
                    ><i class="icon-user"></i>Login</a
                  >
                </li>
              </ul>
            </li>
          </ul>
          <!-- End .top-menu -->
        </div>
        <!-- End .header-right -->
      </div>
      <!-- End .container -->
    </div>
    <!-- End .header-top -->

    <div class="header-middle sticky-header">
      <div class="container-lg">
        <div class="header-left">
          <button class="mobile-menu-toggler">
            <span class="sr-only">Toggle mobile menu</span>
            <i class="icon-bars"></i>
          </button>

          <router-link
            :to="{name: 'home'}"
            class="logo"
          >
            <img
              src="../assets/images/demos/demo-20/logo.png"
              alt="Molla Logo"
              width="100"
              height="25"
            />
          </router-link>

          <nav class="main-nav">
            <ul class="menu sf-arrows">
              <MenuTree
                v-for="(item, index) in menu"
                :key="index"
                :menu="item"
                :depth="1"
              />
            </ul>
            <!-- End .menu -->
          </nav>
          <!-- End .main-nav -->
        </div>
        <!-- End .header-left -->

        <div class="header-right">
          <div class="header-search">
            <a href="#" class="search-toggle" role="button" title="Search"
              ><i class="icon-search text-3xl"></i
            ></a>
            <form action="#" method="get">
              <div class="header-search-wrapper">
                <label for="q" class="sr-only">Search</label>
                <input
                  type="search"
                  class="form-control"
                  name="q"
                  id="q"
                  placeholder="Search in..."
                  required
                />
              </div>
              <!-- End .header-search-wrapper -->
            </form>
          </div>
          <!-- End .header-search -->
          <div class="wishlist relative ps-3 pe-4" v-if="route.name !== 'Cart'">
            <a href="wishlist.html" title="Wishlist">
              <i class="fa-regular fa-heart text-2xl"></i>
              <span class="wishlist-count">3</span>
            </a>
          </div>
          <!-- End .compare-dropdown -->

          <div class="dropdown cart-dropdown ps-3 pe-4" v-if="route.name !== 'Cart'">
            <router-link
              :to="{name: 'Cart'}"
              class="dropdown-toggle"
              role="button"
              data-toggle="dropdown"
              aria-haspopup="true"
              aria-expanded="false"
              data-display="static"
            >
            <i class="las la-shopping-cart text-3xl"></i>
              <span class="cart-count">{{ carts.length }}</span>
            </router-link>

            <CartModal :carts="carts" />
            <!-- End .dropdown-menu -->
          </div>
          <!-- End .cart-dropdown -->
        </div>
        <!-- End .header-right -->
      </div>
      <!-- End .container -->
    </div>
    <!-- End .header-middle -->
  </header>
  <!-- End .header -->
</template>
<script setup>
import { onBeforeMount, computed, ref } from "vue";
import { useCategoryStore } from "@/stores/category";
import { useMenuStore } from "@/stores/menu";
import CategoryTree from "./CategoryTree.vue";
import MenuTree from "./MenuTree.vue";
import CartModal from "@/components/CartModal.vue";
import { useRoute } from "vue-router";

import { useCartStore } from "@/stores/cart";
const cartStore = useCartStore();
const addToCart = async () => {
  await cartStore.addToCart({
    skuId: skuSelect.value.id,
    quantity: quantity.value,
  });
};

const route = useRoute();

const categoryStore = useCategoryStore();
const menuStore = useMenuStore();

const categories = computed(() => categoryStore.$state.listTree.data);
const menu = computed(() => menuStore.$state.menu.data);
const header = ref(null);

const carts = computed(() => cartStore.$state.carts.data);

onBeforeMount(async () => {
  window.onscroll = function () {
    if (
      document.body.scrollTop > 80 ||
      document.documentElement.scrollTop > 80
    ) {
      document.getElementById("header").classList.add("nav-strict");
    } else {
      document.getElementById("header").classList.remove("nav-strict");
    }
  };

  // await categoryStore.getListCategory({ notUse: false });
  await menuStore.getMenu({ notUse: false });
  await cartStore.getList()
});
</script>
<style lang="scss" scoped>
.dropdown-menu {
  position: absolute;
  top: 100%;
  z-index: 1000;
}

.product {
  // padding: 0.5rem 1rem;
  // display: flex;
  // align-items: center;
  cursor: pointer;

  // &:hover {
  //   background-color: aliceblue;
  // }
}

.dropdown-menu {
  ul li {
    a {
      font-weight: 550 !important;
    }

    &:hover {
      background-color: rgb(242, 242, 247);
    }
  }
}

.nav-strict {
  position: fixed;
  left: 0;
  right: 0;
  top: 0;
  z-index: 1040;
  animation-name: fixedHeader;
  animation-duration: 0.4s;
  background-color: #fff;
  box-shadow: 0 0 3px 5px rgba(51, 51, 51, 0.199);
  .logo {
    // display:none;
  }
  // margin-bottom: 70px;
  .container {
    height: 60px;
    z-index: 900000;

    .header-search-wrapper .search-wrapper-wide {
      height: 2rem;
    }

    // input{

    //     padding: 1.8rem 2rem 1rem 0;
    // }
    // button{
    //     padding: .2rem 2rem 1rem .5rem;
    // }
  }
}
</style>
