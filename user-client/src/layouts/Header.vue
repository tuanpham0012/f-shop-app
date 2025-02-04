<template>
  <header class="header header-intro-clearance header-4">
    <div class="header-top" v-if="false">
      <div class="container">
        <div class="header-left">
          <a href="tel:#"><i class="icon-phone"></i>Call: +0123 456 789</a>
        </div>
        <!-- End .header-left -->
        <div class="header-right">
          <ul class="top-menu">
            <li>
              <a href="#">Links</a>
              <ul>
                <li>
                  <div class="header-dropdown">
                    <a href="#">USD</a>
                    <div class="header-menu">
                      <ul>
                        <li><a href="#">Eur</a></li>
                        <li><a href="#">Usd</a></li>
                      </ul>
                    </div>
                    <!-- End .header-menu -->
                  </div>
                </li>
                <li>
                  <div class="header-dropdown">
                    <a href="#">English</a>
                    <div class="header-menu">
                      <ul>
                        <li><a href="#">English</a></li>
                        <li><a href="#">French</a></li>
                        <li><a href="#">Spanish</a></li>
                      </ul>
                    </div>
                    <!-- End .header-menu -->
                  </div>
                </li>
                <!-- {{-- <li><a href="#signin-modal" data-toggle="modal">Sign in / Sign up</a></li> --}} -->
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

    <div class="header-middle" id="header-middle">
      <div class="container justify-content-center">
        <div class="header-left me-5">
          <!-- <div
            class="dropdown category-dropdown d-inline w-100 d-none d-lg-block"
          >
            <a
              href="#"
              class="dropdown-toggle fw-550"
              type="button"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              Danh mục <i class="icon-angle-down"></i>
            </a>
            <div class="dropdown-menu">
              <nav class="side-nav">
                <ul class="menu-vertical sf-arrows">
                  <CategoryTree v-for="item in categories" :entry="item" />
                </ul>
              </nav>
            </div>
          </div> -->
          <button class="mobile-menu-toggler">
            <span class="sr-only">Toggle mobile menu</span>
            <i class="icon-bars"></i>
          </button>
          <div class="w-[150px] flex-0">
            <a href="/" class="logo">
              <img
                src="../assets/images/demos/demo-4/logo.png"
                alt="Molla Logo"
                width="105"
                height="25"
              />
            </a>
          </div>
        </div>
        <!-- End .header-left -->

        <!-- End .header-left -->

        <div class="header-center">
          <div
            class="header-search header-search-extended header-search-visible d-none d-lg-block"
          >
            <a href="#" class="search-toggle" role="button"
              ><i class="icon-search"></i
            ></a>
            <form action="#" method="get">
              <div class="header-search-wrapper search-wrapper-wide w-500px">
                <label for="key-word" class="sr-only">Tìm kiếm</label>
                <button class="btn btn-primary" type="submit">
                  <i class="icon-search"></i>
                </button>
                <input
                  type="search"
                  class="form-control"
                  name="key-word"
                  id="key-word"
                  placeholder="Tìm kiếm sản phẩm..."
                  required
                />
              </div>
              <!-- End .header-search-wrapper -->
            </form>
          </div>
          <!-- End .header-search -->
        </div>

        <div class="header-right">
          <div class="dropdown cart-dropdown">
            <a
              href="#"
              class="dropdown-toggle"
              role="button"
              data-toggle="dropdown"
              aria-haspopup="true"
              aria-expanded="false"
              data-display="static"
            >
              <div class="icon">
                <i class="icon-shopping-cart"></i>
                <span class="cart-count">2</span>
              </div>
            </a>

            <div class="dropdown-menu dropdown-menu-right w-400px p-0">
              <p class="p-3 pb-3 fs-4 fw-500 text-gray">Giỏ hàng</p>
              <div class="dropdown-cart-products">
                <div class="product">
                  <div class="product-cart-details max-w-300px">
                    <h4 class="product-title fs-4">
                      <a href="product.html"
                        >Beige knitted elastic runner shoes</a
                      >
                    </h4>

                    <span class="cart-product-info fs-5 text-danger">
                      <span class="cart-product-qty fs-5 text-danger">1</span>
                      x 70000đ
                    </span>
                  </div>

                  <figure class="product-image-container">
                    <div class="product-image w-50px h-50px">
                      <img
                        src="../assets/images/products/cart/product-1.jpg"
                        alt="product"
                      />
                    </div>
                  </figure>
                </div>
                <!-- End .product -->
              </div>
              <!-- End .cart-product -->

              <div class="dropdown-cart-total p-4">
                <span>Total</span>

                <span class="cart-total-price">$160.00</span>
              </div>
              <!-- End .dropdown-cart-total -->

              <div class="dropdown-cart-action p-4">
                <a href="cart.html" class="btn btn-primary">Xem giỏ hàng</a>
                <a href="checkout.html" class="btn btn-outline-primary-2"
                  ><span>Thanh toán</span><i class="icon-long-arrow-right"></i
                ></a>
              </div>

              <!-- End .dropdown-cart-total -->
            </div>
            <!-- End .dropdown-menu -->
          </div>
          <!-- End .cart-dropdown -->

          <div class="mx-4">
            <i class="fa-regular fa-user fs-2"></i>
          </div>
          <!-- End .user-dropdown -->
          <!-- End .compare-dropdown -->
        </div>
        <!-- End .header-right -->
      </div>
      <!-- End .container -->
    </div>
    <!-- End .header-middle -->

    <div class="header-bottom sticky-header">
      <div class="container">
        <div class="header-left">
          <div class="dropdown category-dropdown d-inline">
            <a
              href="#"
              class="dropdown-toggle"
              type="button"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              Danh mục <i class="icon-angle-down"></i>
            </a>
            <div class="dropdown-menu">
              <nav class="side-nav">
                <ul class="menu-vertical sf-arrows">
                  <CategoryTree v-for="item in categories" :entry="item" />
                </ul>
              </nav>
            </div>
          </div>
        </div>
        <!-- End .header-left -->

        <div class="header-center">
          <nav class="main-nav">
            <ul class="menu sf-arrows">
              <li class="megamenu-container">
                <a href="index.html" class="sf-with-ul">Home</a>
              </li>
              <li>
                <a href="category.html" class="sf-with-ul">Shop</a>
              </li>
              <li>
                <a href="product.html" class="sf-with-ul">Product</a>
              </li>
              <li>
                <a href="#" class="sf-with-ul">Pages</a>

                <ul>
                  <li>
                    <a href="about.html" class="sf-with-ul">About</a>
                    <ul>
                      <li>
                        <a href="about.html">About 01</a>
                      </li>
                      <li>
                        <a href="about-2.html">About 02</a>
                      </li>
                      <li>
                        <a href="about.html" class="sf-with-ul">About 01</a>
                        <ul>
                          <li>
                            <a href="about.html">About 01</a>
                          </li>
                          <li>
                            <a href="about-2.html">About 02</a>
                          </li>
                          <li>
                            <a href="about.html">About 01</a>
                          </li>
                        </ul>
                      </li>
                    </ul>
                  </li>
                  <li>
                    <a href="contact.html" class="sf-with-ul">Contact</a>

                    <ul>
                      <li>
                        <a href="contact.html">Contact 01</a>
                      </li>
                      <li>
                        <a href="contact-2.html">Contact 02</a>
                      </li>
                    </ul>
                  </li>
                  <li><a href="login.html">Login</a></li>
                  <li><a href="faq.html">FAQs</a></li>
                  <li><a href="404.html">Error 404</a></li>
                  <li>
                    <a href="coming-soon.html">Coming Soon</a>
                  </li>
                </ul>
              </li>
              <li>
                <a href="blog.html" class="sf-with-ul">Blog</a>

                <ul>
                  <li><a href="blog.html">Classic</a></li>
                  <li>
                    <a href="blog-listing.html">Listing</a>
                  </li>
                  <li>
                    <a href="#">Grid</a>
                    <ul>
                      <li>
                        <a href="blog-grid-2cols.html">Grid 2 columns</a>
                      </li>
                      <li>
                        <a href="blog-grid-3cols.html">Grid 3 columns</a>
                      </li>
                      <li>
                        <a href="blog-grid-4cols.html">Grid 4 columns</a>
                      </li>
                      <li>
                        <a href="blog-grid-sidebar.html">Grid sidebar</a>
                      </li>
                    </ul>
                  </li>
                  <li>
                    <a href="#">Masonry</a>
                    <ul>
                      <li>
                        <a href="blog-masonry-2cols.html">Masonry 2 columns</a>
                      </li>
                      <li>
                        <a href="blog-masonry-3cols.html">Masonry 3 columns</a>
                      </li>
                      <li>
                        <a href="blog-masonry-4cols.html">Masonry 4 columns</a>
                      </li>
                      <li>
                        <a href="blog-masonry-sidebar.html">Masonry sidebar</a>
                      </li>
                    </ul>
                  </li>
                  <li>
                    <a href="#">Mask</a>
                    <ul>
                      <li>
                        <a href="blog-mask-grid.html">Blog mask grid</a>
                      </li>
                      <li>
                        <a href="blog-mask-masonry.html">Blog mask masonry</a>
                      </li>
                    </ul>
                  </li>
                  <li>
                    <a href="#">Single Post</a>
                    <ul>
                      <li>
                        <a href="single.html">Default with sidebar</a>
                      </li>
                      <li>
                        <a href="single-fullwidth.html">Fullwidth no sidebar</a>
                      </li>
                      <li>
                        <a href="single-fullwidth-sidebar.html"
                          >Fullwidth with sidebar</a
                        >
                      </li>
                    </ul>
                  </li>
                </ul>
              </li>
              <li>
                <a href="elements-list.html" class="sf-with-ul">Elements</a>

                <ul>
                  <li>
                    <a href="elements-products.html">Products</a>
                  </li>
                  <li>
                    <a href="elements-typography.html">Typography</a>
                  </li>
                  <li>
                    <a href="elements-titles.html">Titles</a>
                  </li>
                  <li>
                    <a href="elements-banners.html">Banners</a>
                  </li>
                  <li>
                    <a href="elements-product-category.html"
                      >Product Category</a
                    >
                  </li>
                  <li>
                    <a href="elements-video-banners.html">Video Banners</a>
                  </li>
                  <li>
                    <a href="elements-buttons.html">Buttons</a>
                  </li>
                  <li>
                    <a href="elements-accordions.html">Accordions</a>
                  </li>
                  <li>
                    <a href="elements-tabs.html">Tabs</a>
                  </li>
                  <li>
                    <a href="elements-testimonials.html">Testimonials</a>
                  </li>
                  <li>
                    <a href="elements-blog-posts.html">Blog Posts</a>
                  </li>
                  <li>
                    <a href="elements-portfolio.html">Portfolio</a>
                  </li>
                  <li>
                    <a href="elements-cta.html">Call to Action</a>
                  </li>
                  <li>
                    <a href="elements-icon-boxes.html">Icon Boxes</a>
                  </li>
                </ul>
              </li>
            </ul>
            <!-- End .menu -->
          </nav>
          <!-- End .main-nav -->
        </div>
        <!-- End .header-center -->

        <div class="header-right">
          <i class="la la-lightbulb-o"></i>
          <p>Clearance<span class="highlight">&nbsp;Up to 30% Off</span></p>
        </div>
      </div>
      <!-- End .container -->
    </div>
    <!-- End .header-bottom -->
  </header>
  <!-- End .header -->
</template>
<script setup>
import { onBeforeMount, computed } from "vue";
import { useCategoryStore } from "@/stores/product";
import CategoryTree from "./CategoryTree.vue";

const categoryStore = useCategoryStore();

const categories = computed(() => categoryStore.$state.listTree.data);

onBeforeMount(async () => {
  window.onscroll = function () {
    if (
      document.body.scrollTop > 80 ||
      document.documentElement.scrollTop > 80
    ) {
      document.getElementById("header-middle").classList.add("nav-strict");
    } else {
      document.getElementById("header-middle").classList.remove("nav-strict");
    }
  };

  await categoryStore.getListTree({ notUse: false });
});
</script>
<style lang="scss" scoped>
.dropdown-menu {
  position: absolute;
  top: 100%;
  z-index: 1000;
  right: -150% !important;
}

.product {
  padding: 0.5rem 1rem;
  display: flex;
  align-items: center;
  cursor: pointer;

  &:hover {
    background-color: aliceblue;
  }
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
