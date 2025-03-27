<template lang="">
  <Editor
    api-key="f4q3fvmukysw5t3hf04p2nd42vv1r41elksdzv8f5zi7kd5w"
    :init="init"
    v-model="inputValue"
    @input="updateValue"
  />
</template>
<script setup lang="ts">
import { computed, ref, onBeforeMount, reactive, watch } from "vue";

const props = defineProps({
    modelValue: {
      type: String,
      default: '',
    },
    placeholder: {
      type: String,
      default: '',
    },
    type: {
      type: String,
      default: 'text',
    },
    id: {
      type: String,
      default: () => `input-${Math.random().toString(36).substring(2, 15)}`,
    },
});

const emits = defineEmits(['update:modelValue']);

const inputValue = ref(props.modelValue);

const updateValue = () => {
    emits('update:modelValue', inputValue.value)
}

watch(() => inputValue.value, (value) => {
    updateValue();
});

watch(() => props.modelValue, (value) => {
    inputValue.value = value;
});

const init = reactive({
  selector: "textarea#open-source-plugins",
  plugins:
    "preview importcss searchreplace autolink autosave save directionality code visualblocks visualchars fullscreen image link media codesample table charmap pagebreak nonbreaking anchor insertdatetime advlist lists wordcount help charmap quickbars emoticons accordion",
  editimage_cors_hosts: ["picsum.photos"],
  menubar: "file edit view insert format tools table help",
  toolbar:
    "undo redo | accordion accordionremove | blocks fontfamily fontsize | bold italic underline strikethrough | align numlist bullist | link image | table media | lineheight outdent indent| forecolor backcolor removeformat | charmap emoticons | code fullscreen preview | save print | pagebreak anchor codesample | ltr rtl",
  autosave_ask_before_unload: true,
  autosave_interval: "30s",
  autosave_prefix: "{path}{query}-{id}-",
  autosave_restore_when_empty: false,
  autosave_retention: "2m",
  image_advtab: true,
  link_list: [
    { title: "My page 1", value: "https://www.tiny.cloud" },
    { title: "My page 2", value: "http://www.moxiecode.com" },
  ],
  image_list: [
    { title: "My page 1", value: "https://www.tiny.cloud" },
    { title: "My page 2", value: "http://www.moxiecode.com" },
  ],
  image_class_list: [
    { title: "None", value: "" },
    { title: "Some class", value: "class-name" },
  ],
  importcss_append: true,
  height: '75vh',
  image_caption: true,
  quickbars_selection_toolbar: 'bold italic | quicklink h2 h3 blockquote quickimage quicktable',
  noneditable_class: 'mceNonEditable',
  toolbar_mode: 'sliding',
  contextmenu: 'link image table',
  skin: 'oxide',
  content_css: 'default', ///src/assets/css/main.css
});
</script>
<style lang=""></style>
