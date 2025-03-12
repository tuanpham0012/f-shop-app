<template>
  <div
    class="w-100 d-flex col-lg-12 m-1 pe-4 flex-wrap justify-content-center justify-content-sm-between"
    
  >
    <div
      class="d-none d-lg-flex align-items-center min-w-[500px] justify-start"
    >
      <div class="d-flex w-[150px]">
        <label for="currenPage" class="col-form-label w-[75px]"
          >SL hiển thị</label
        >
        <div class="">
          <select
            class="form-select form-select-sm"
            data-placeholder="Select an option"
            v-model="pageSize"
          >
            <option :value="10">10</option>
            <option :value="25">25</option>
            <option :value="50">50</option>
            <!-- <option :value="100">100</option>
            <option :value="150">150</option> -->
          </select>
        </div>
      </div>
      <div class="d-flex w-[250px]">
        <label for="currenPage" class="col-form-label"
          >Hiển thị
          {{ (currentPage - 1) * pageSize + 1 }}
          -
          {{ ((currentPage - 1) * pageSize + pageSize) > props.totalCount ? props.totalCount : ((currentPage - 1) * pageSize + pageSize) }} 
          / {{ props.totalCount }} bản ghi.</label
        >
      </div>
    </div>
    <div class="d-flex gap-2 flex-wrap">
      <div>
        <ul class="pagination pagination-outline align-items-center pt-3 me-5">
          <li
            class="page-item previous"
            v-if="props.currentPage > 1"
            @click.prevent="changePage((currentPage -= 1))"
          >
            <a href="#" class="page-link"
              ><i class="bx bxs-chevrons-left bx-fade-left-hover"></i
            ></a>
          </li>
          <li
            class="page-item"
            v-for="(page, index) in pageView(props.totalPages)"
            :key="index"
            :class="{
              'active': page == props.currentPage,
              'disabled': typeof page == 'string',
            }"
            @click.prevent="changePage(page)"
          >
            <a href="#" class="page-link">{{ page }}</a>
          </li>
          <li
            class="page-item next"
            v-if="props.currentPage < props.totalPages"
            @click.prevent="changePage((currentPage += 1))"
          >
            <a href="#" class="page-link"
              ><i class="bx bxs-chevrons-right bx-fade-right-hover"></i
            ></a>
          </li>
        </ul>
      </div>
      <div class="d-flex g-3 align-items-center max-w-[175px]">
        <div class="max-w-[100px] pe-2">
          <label for="toPage" class="col-form-label">Tới trang</label>
        </div>
        <div class="p-0 max-w-[50px]">
          <input
            type="text"
            class="form-control"
            @keypress="filter($event)"
            :value="currentPage"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { watch, ref } from "vue";
import debounce from "lodash.debounce";

const props = defineProps({
  // pagination: {
  //     type: Object,
  //     default: { current_page: 1, total_pages: 1, per_page: 30, total: 0 },
  // },
  currentPage: {
    type: Number,
    default: 1,
  },
  totalPages: {
    type: Number,
    default: 1,
  },
  pageSize: {
    type: [Number],
    default: 30,
  },
  totalCount: {
    type: Number,
    default: 1,
  },
});

const pageSize = ref(props.pageSize);
const currentPage = ref(props.currentPage);

const emits = defineEmits(["change-page"]);

const changePage = (page) => {
  console.log("changePage");
  
  if (page < 1) {
    return;
  }
  if (page > props.totalPages) {
    return;
  }
  // currentPage.value = page;
  emits("change-page", {
    currentPage: page,
    totalPages: props.totalPages,
    pageSize: pageSize.value,
    totalCount: props.totalCount,
  });
};

watch(pageSize.value, (newValue, oldValue) => {
  changePage(1)
});

watch(
  () => props.currentPage,
  (newValue, oldValue) => {
    currentPage.value = newValue;
  }
);

watch(
  () => currentPage.value,
  debounce((newValue, oldValue) => {
    if (newValue >= props.totalPages) {
      currentPage.value = props.totalPages;
    }
    // if(oldValue != newValue)
    //   changePage(currentPage.value);
  },400)
);

const pageView = (totalPages, delta = 1) => {
  const truncate = true;
  const curPage = parseInt(props.currentPage);

  const range = delta + 4; // use for handle visible number of links left side

  let render = [];
  let renderTwoSide = [];
  let dot = "...";
  let countTruncate = 0; // use for ellipsis - truncate left side or right side

  // use for truncate two side
  const numberTruncateLeft = curPage - delta;
  const numberTruncateRight = curPage + delta;

  for (let pos = 1; pos <= totalPages; pos++) {
    // truncate
    if (totalPages >= 2 * range - 1 && truncate) {
      if (numberTruncateLeft > 3 && numberTruncateRight < totalPages - 3 + 1) {
        // truncate 2 side
        if (pos >= numberTruncateLeft && pos <= numberTruncateRight) {
          renderTwoSide.push(pos);
        }
      } else {
        // truncate left side or right side
        if (
          (curPage < range && pos <= range) ||
          (curPage > totalPages - range && pos >= totalPages - range + 1) ||
          pos === totalPages ||
          pos === 1
        ) {
          render.push(pos);
        } else {
          countTruncate++;
          if (countTruncate === 1) render.push(dot);
        }
      }
    } else {
      // not truncate
      render.push(pos);
    }
  }
  if (renderTwoSide.length > 0) {
    return [1, dot, ...renderTwoSide, dot, totalPages];
  } else {
    return render;
  }
};

const filter = (evt) => {
  evt = evt ? evt : window.event;
  var charCode = evt.which ? evt.which : evt.keyCode;
  if (charCode == 13 && currentPage.value != props.pagination.current_page) {
    changePage(currentPage.value);
  }
  if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode !== 46) {
    evt.preventDefault();
  } else {
    return true;
  }
};
</script>
<style lang="scss" scoped>
.active, .disabled {
  pointer-events: none;
}

label {
  &.col-form-label {
    font-size: 12px;
  }
}

input {
  &.form-control {
    font-size: 12px;
  }
}

li {
  &.page-item {
    padding: 0 !important;
    a {
      &.page-link {
        font-size: 12px;
        // width: 2rem !important;
        min-width: 2rem !important;
        height: 2rem !important;
      }
    }
  }
}
</style>
