#pragma once
#include "external/atomic_queue/atomic_queue.h"
#include "std_allocator.h"

template <class T>
using ParallelQueueAllocator = GodotStdAllocator<T>;

template <class T, unsigned SIZE, bool MINIMIZE_CONTENTION = true, bool MAXIMIZE_THROUGHPUT = true, bool TOTAL_ORDER = false, bool SPSC = false>
using StaticParallelQueue = atomic_queue::AtomicQueue2<T, SIZE, MINIMIZE_CONTENTION, MAXIMIZE_THROUGHPUT, TOTAL_ORDER, SPSC>;

template <class T, class A = ParallelQueueAllocator<T>, bool MAXIMIZE_THROUGHPUT = true, bool TOTAL_ORDER = false, bool SPSC = false>
using ParalellQueue = atomic_queue::AtomicQueueB2<T, A, MAXIMIZE_THROUGHPUT, TOTAL_ORDER, SPSC>;
