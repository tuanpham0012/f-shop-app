<template lang="">
  <!-- Mobile Menu -->
  <!-- <div class="mobile-menu-overlay"></div> -->
  <!-- End .mobil-menu-overlay -->

  <div class="mobile-menu-container mobile-menu-light">
    <div class="mobile-menu-wrapper">
      <span class="mobile-menu-close"><i class="icon-close"></i></span>

      <form action="#" method="get" class="mobile-search">
        <label for="mobile-search" class="sr-only">Search</label>
        <input
          type="search"
          class="form-control"
          name="mobile-search"
          id="mobile-search"
          placeholder="Search in..."
          required
        />
        <button class="btn btn-primary" type="submit">
          <i class="icon-search"></i>
        </button>
      </form>

      <div class="tab-content">
        <div
          class="tab-pane fade show active sidebar"
          id="mobile-cats-tab"
          role="tabpanel"
          aria-labelledby="mobile-cats-link"
        >
          <nav class="mobile-cats-nav">
            <ul class="mobile-cats-menu">
              <li
                v-for="item in menuItems"
                :key="item.id"
                :class="{ 'has-submenu': item.children, 'active': item.showSubmenu }"
              >
                <a href="#" @click.prevent="toggleSubmenu(item)">
                  <i :class="item.icon"></i> {{ item.label }}
                  <i
                    v-if="item.children"
                    class="fa-solid fa-chevron-right"
                    style="float: right"
                  ></i>
                </a>
                <ul
                  v-if="item.children"
                  class="submenu"
                  
                >
                  <li v-for="child in item.children" :key="child.id">
                    <a href="#">
                      <i :class="child.icon"></i> {{ child.label }}
                    </a>
                  </li>
                </ul>
              </li>
            </ul>
            <!-- End .mobile-cats-menu -->
          </nav>
          <!-- End .mobile-cats-nav -->
        </div>
        <!-- .End .tab-pane -->
      </div>
      <!-- End .tab-content -->

      <div class="social-icons">
        <a href="#" class="social-icon" target="_blank" title="Facebook"
          ><i class="icon-facebook-f"></i
        ></a>
        <a href="#" class="social-icon" target="_blank" title="Twitter"
          ><i class="icon-twitter"></i
        ></a>
        <a href="#" class="social-icon" target="_blank" title="Instagram"
          ><i class="icon-instagram"></i
        ></a>
        <a href="#" class="social-icon" target="_blank" title="Youtube"
          ><i class="icon-youtube"></i
        ></a>
      </div>
      <!-- End .social-icons -->
    </div>
    <!-- End .mobile-menu-wrapper -->
  </div>
  <!-- End .mobile-menu-container -->
</template>
<script lang="ts" setup>
import { onBeforeMount, ref } from "vue";

const menuItems = ref([
  { id: 1, label: "Dashboard", icon: "fas fa-tachometer-alt" },
  {
    id: 2,
    label: "Products",
    icon: "fas fa-box",
    children: [
      { id: 21, label: "Add Product", icon: "fas fa-plus" },
      { id: 22, label: "View Products", icon: "fas fa-list" },
      { id: 23, label: "Manage Categories", icon: "fas fa-folder" },
    ],
    showSubmenu: false,
  },
  { id: 3, label: "Customers", icon: "fas fa-users" },
  {
    id: 4,
    label: "Orders",
    icon: "fas fa-shopping-cart",
    children: [
      { id: 41, label: "New Orders", icon: "fas fa-file-invoice" },
      { id: 42, label: "Order History", icon: "fas fa-history" },
    ],
    showSubmenu: false,
  },
  { id: 5, label: "Settings", icon: "fas fa-cog" },
  { id: 6, label: "Help", icon: "fas fa-question-circle" },
]);

const toggleSubmenu = (item: any) => {
  item.showSubmenu = !item.showSubmenu;
};

onBeforeMount(() => {});
</script>
<style lang="scss" scoped>

.sidebar ul {
  list-style: none;
  padding: 0;
  margin: 0;
}

.sidebar li a {
  padding: 15px 25px;
  text-decoration: none;
  font-size: 16px;
  display: block;
}

.sidebar li a:hover {
  background-color: #98bcff80;
}

.sidebar .submenu {
  display: none; /* Ẩn submenu mặc định */
}

.sidebar .active {
  >.submenu{
    display: block;
  }
  .fa-chevron-right {
      transform: rotate(90deg);
      transition-duration: .4s;
  }
}

.sidebar .submenu a {
  padding-left: 40px;
  font-size: 14px;
}

.sidebar i {
  margin-right: 5px;
}

.has-submenu > a {
  position: relative; /* Để định vị mũi tên chevron bên trong */
}

.content {
  margin-left: 250px;
  padding: 20px;
}
.has-submenu {
  ul {
    animation: slidein 0.7s;
  }
}

@keyframes slidein {
  0% {
    opacity: 0;
  }
  25% {
    opacity: 0.25;
  }
  50% {
    opacity: 0.5;
  }
  100% {
    opacity: 1;
  }
}
</style>
