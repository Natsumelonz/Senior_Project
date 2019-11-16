<template>
    <v-app>
        
        <div class="dashboard ml-5 mt-2 ">
            <v-layout row>
            <h1 class="grey--text heder ">List of Words</h1>
                    <v-dialog v-model="dialog" width="500">
                        <template v-slot:activator="{ on }">
                            <v-btn class="heder_btn" color="red lighten-2" dark v-on="on">Add Word</v-btn>
                        </template>
                        <v-card>
                            <v-card-title class="headline orange white--text" primary-title>Add a New Word</v-card-title>
                                <v-form class="px-3">
                                    <v-text-field label="Word(JP)" v-model="wordname_JP"></v-text-field>
                                    <v-text-field label="Word(Romaji)" v-model="wordname_romanji"></v-text-field>
                                    <v-text-field label="Meaning" v-model="word_meaning"></v-text-field>
                                    <v-text-field label="Number of Syllable (Ex. sakura is 3)" v-model="syllable_number"></v-text-field>
                                    <v-card-action>
                                         <v-btn text class="orange white--text mb-3" @click="submit">Add</v-btn>
                                    </v-card-action>
                                   
                                </v-form>

                        </v-card>
                    </v-dialog>
            </v-layout>
            <v-container class="card">
                
                <v-card flat class="pa-3" v-for="word in words" :key="word.word_id">
                    <v-layout row wrap>
                        <v-flex xs6 sm4 md2 class="ml-3">
                            <div class="caption grey--text ml-3">No.</div>
                            <div class="ml-3">{{word.word_id}}</div>
                        </v-flex>
                        <v-flex xs6 sm4 md2>
                            <div class="caption grey--text">name(Japanese)</div>
                            <div>{{word.wordname_JP}}</div>
                        </v-flex>
                        <v-flex xs6 sm4 md2>
                            <div class="caption grey--text">name(Romaji)</div>
                            <div>{{word.wordname_romanji}}</div>
                        </v-flex>
                        <v-flex xs6 sm4 md2>
                            <div class="caption grey--text">Meaning</div>
                            <div>{{word.word_meaning}}</div>
                        </v-flex>
                        <v-flex xs6 sm4 md2>
                            <div class="caption grey--text">Syllable No.</div>
                            <div>{{word.syllable_number}}</div>
                        </v-flex>
                    </v-layout>
                </v-card>
            </v-container>
        </div>
        
    </v-app>
    
</template>

<script>
import axios from "axios";

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
    mounted() {
            axios
            .get("https://it59-28yomimasu.firebaseio.com/Word.json").then(response =>{
                this.words = response.data
                this.words_size = response.data.length
            })
    },
    methods: {
        
        submit() {
            axios
            .put("https://it59-28yomimasu.firebaseio.com/Word/"+this.words_size+".json", {
            word_id: this.words_size + 1,
            wordname_JP: this.wordname_JP,
            wordname_romanji: this.wordname_romanji,
            word_meaning: this.word_meaning,
            syllable_number: this.syllable_number
            }
            );
            this.dialog = false;
            window.location.reload()
        }
    }
}
</script>

<style>

.heder{
    margin-left: 235px;
    margin-top: 10px;
}

.heder_btn{
    margin-left: 25px;
    margin-top: 18px;
}

.card{
    overflow: auto;
    max-height: 700px;
    margin-top: 12px;
}

.pagefx{
    position: fixed;
}
</style>