package main

import (
	"bytes"
	"fmt"
	"log"
	"os"
	"path"
	"runtime"
	"sort"
	"sync"
	"time"
)

type fileInfo struct {
	first        []byte
	middle       []byte
	last         []byte
	total        []byte
	size         int64
	isDuplicated bool
}

func printFile(path string, info os.FileInfo, err error) error {
	if err != nil {
		log.Print(err)
		return nil
	}
	fmt.Println(path)
	return nil
}

func readdir(dirName string, wg *sync.WaitGroup, files *[]string) {
	f, err := os.Open(dirName)
	if err != nil {
		fmt.Println(err)
		os.Exit(1)
	}

	names, err := f.Readdirnames(-1)
	f.Close()
	if err != nil {
		fmt.Println(err)
		os.Exit(1)
	}
	sort.Strings(names)

	for _, element := range names {
		fullpath := path.Join(dirName, element)
		fi, err := os.Stat(fullpath)
		if err != nil {
			fmt.Println(err)
			continue
		}
		switch mode := fi.Mode(); {
		case mode.IsDir():
			wg.Add(1)
			go readdir(fullpath, wg, files)
		case mode.IsRegular():
			*files = append(*files, fullpath)
		}
	}
	wg.Done()
}

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func seekAtPosition(f *os.File, position int64) []byte {
	checksum, err := f.Seek(position, 0)
	check(err)

	b2 := make([]byte, 10)
	n2, err := f.Read(b2)
	check(err)

	if checksum < 0 && n2 < 0 {
		check(err)
	}

	return b2
}

func readfile(fileName string, filesInfo map[string]fileInfo) {
	f, err := os.Open(fileName)

	fi, err := f.Stat()
	check(err)
	size := fi.Size()

	first := seekAtPosition(f, 0)
	middle := seekAtPosition(f, size/2)
	last := seekAtPosition(f, size-50)
	tempTotal := [][]byte{first, middle, last}
	total := bytes.Join(tempTotal, []byte("-"))

	filesInfo[fileName] = fileInfo{size: size, first: first, middle: middle, last: last, total: total}
}

func compareFilesInfo(element1 fileInfo, element2 fileInfo) {
	if bytes.Equal(element1.first, element2.first) {
		if bytes.Equal(element1.middle, element2.middle) {
			if bytes.Equal(element1.last, element2.last) {
				if element1.size == element2.size {
					element1.isDuplicated = true
					element2.isDuplicated = true
				}
			}
		}
	}
}

func compareFilesInfoMap(filesInfo map[string]fileInfo) {
	for i := range filesInfo {
		for j := range filesInfo {
			if i == j {
				continue
			}
			compareFilesInfo(filesInfo[i], filesInfo[j])
		}
	}
}

func main() {
	var files []string
	filesInfo := make(map[string]fileInfo)

	var wg sync.WaitGroup
	start := time.Now()
	dir := os.Args[1]

	runtime.GOMAXPROCS(runtime.NumCPU())
	log.SetFlags(log.Lshortfile)

	wg.Add(1)
	go readdir(dir, &wg, &files)
	wg.Wait()

	log.Println(len(files))

	for _, element := range files {
		readfile(element, filesInfo)
	}

	compareFilesInfoMap(filesInfo)

	elapsed := time.Since(start)
	fmt.Print(filesInfo)
	log.Printf("time %s", elapsed)
}
