<template>
    <v-app>
        <div class="dashboard ml-5 mt-2 ">
            <v-container>
                <h1 class="subheading grey--text">List of Words</h1>
                <v-card flat class="pa-3 mt-2" v-for="word in words" :key="word.word_id">
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
                            <div>{{word.word_syllable}}</div>
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
            words: null
        }     
    },
    mounted() {
            axios
            .get("https://it59-28yomimasu.firebaseio.com/Word.json").then(response =>{
                this.words = response.data
            })
    }
}
</script>