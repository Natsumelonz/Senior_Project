<template>
  <v-dialog v-model="dialog" width="500">
                        <template v-slot:activator="{ on }">
                            <v-btn color="red lighten-2" dark v-on="on">Add Word</v-btn>
                        </template>
                        <v-card>
                            <v-card-title class="headline grey lighten-2" primary-title>Add a New Word</v-card-title>
                                <v-form class="px-3">
                                    <v-text-field label="Word(JP)" v-model="wordname_JP"></v-text-field>
                                    <v-text-field label="Word(Romaji)" v-model="wordname_romanji"></v-text-field>
                                    <v-text-field label="Meaning" v-model="word_meaning"></v-text-field>
                                    <v-text-field label="Number of Syllable (Ex. sakura is 3)" v-model="syllable_number"></v-text-field>
                                    <v-btn text class="success mx-0 mb-3" @click="addWord">Add Word</v-btn>
                                </v-form>
                            
                        </v-card>
                    </v-dialog>
</template>

<script>
  export default {
    data() { 
        return {
            words: null,
            dialog: false,
            word_id: null,
            words_size: null,
            wordname_JP: '',
            wordname_romanji: '',
            word_meaning: '',
            syllable_number: ''
        }     
    },
    methods: {
        submit() {
            axios
            .post("https://it59-28yomimasu.firebaseio.com/Word.json", {
            word_id: 000,
            wordname_JP: 'あめ',
            wordname_romanji: 'ame',
            word_meaning: 'rain',
            syllable_number: 2
            }
            )
            .then(this.message='Added successfully');
        }
    }
  }
</script>