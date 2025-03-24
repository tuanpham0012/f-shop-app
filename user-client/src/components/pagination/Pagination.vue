<template>
  <nav aria-label="Page navigation">
    <ul class="pagination justify-content-center">
      <li
        class="page-item"
        v-if="props.currentPage > 1"
        @click.prevent="changePage((currentPage -= 1))"
      >
        <a
          class="page-link page-link-prev"
          href="#"
          aria-label="Previous"
          tabindex="-1"
          aria-disabled="true"
        >
          <span aria-hidden="true"><i class="icon-long-arrow-left"></i></span
          >Prev
        </a>
      </li>
      <li
        class="page-item"
        aria-current="page"
        v-for="(page, index) in pageView(props.totalPages)"
        :key="index"
        :class="{
          'active': page == props.currentPage,
          'disabled': typeof page == 'string',
        }"
        @click.prevent="changePage(page)"
      >
        <a class="page-link" href="#">{{ page }}</a>
      </li>
      <li
        class="page-item"
        v-if="props.currentPage < props.totalPages"
        @click.prevent="changePage((currentPage += 1))"
      >
        <a class="page-link page-link-next" href="#" aria-label="Next">
          Next
          <span aria-hidden="true"><i class="icon-long-arrow-right"></i></span>
        </a>
      </li>
    </ul>
  </nav>
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
  if (page < 1) {
    return;
  }
  if (page > props.totalPages) {
    return;
  }
  currentPage.value = page;
  emits("change-page", {
    currentPage: currentPage.value,
    totalPages: props.totalPages,
    pageSize: pageSize.value,
    totalCount: props.totalCount,
  });
};

watch(pageSize, (newValue, oldValue) => {
  currentPage.value = 1;
  emits("change-page", {
    currentPage: currentPage.value,
    totalPages: props.totalPages,
    pageSize: pageSize.value,
    totalCount: props.totalCount,
  });
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
    changePage(currentPage.value);
  }, 400)
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

</script>
<style lang="scss" scoped>
.active,
.disabled {
  pointer-events: none;
}

label {
  &.col-form-label {
    font-size: 12px;
  }
}
</style>
