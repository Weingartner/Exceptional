
using System;
using System.Collections.Generic;
using Weingartner.Exceptional;
using System.Reactive.Linq;
using System.Reactive;

namespace Weingartner.Exceptional.Reactive {
    public static partial class ObservableExceptional {

    
		/// <Summary>
		/// If any of the inputs are in error then the output will contain an aggregate
		/// exception of the inputs. If the selector function throws then the output will
		/// contain that exception.
		/// </Summary>
		public static IObservableExceptional<TU> CombineLatest<T1, T2, TU>
			(this IObservableExceptional<T1> t1, IObservableExceptional<T2> t2
			, Func<T1, T2, TU> fn)
		{
			return Observable.CombineLatest(
						t1.Observable,
						t2.Observable,
						 (v1, v2)=>Exceptional.Combine(v1, v2,fn)
			).ToObservableExceptional();

		}

    
		/// <Summary>
		/// If any of the inputs are in error then the output will contain an aggregate
		/// exception of the inputs. If the selector function throws then the output will
		/// contain that exception.
		/// </Summary>
		public static IObservableExceptional<TU> CombineLatest<T1, T2, T3, TU>
			(this IObservableExceptional<T1> t1, IObservableExceptional<T2> t2, IObservableExceptional<T3> t3
			, Func<T1, T2, T3, TU> fn)
		{
			return Observable.CombineLatest(
						t1.Observable,
						t2.Observable,
						t3.Observable,
						 (v1, v2, v3)=>Exceptional.Combine(v1, v2, v3,fn)
			).ToObservableExceptional();

		}

    
		/// <Summary>
		/// If any of the inputs are in error then the output will contain an aggregate
		/// exception of the inputs. If the selector function throws then the output will
		/// contain that exception.
		/// </Summary>
		public static IObservableExceptional<TU> CombineLatest<T1, T2, T3, T4, TU>
			(this IObservableExceptional<T1> t1, IObservableExceptional<T2> t2, IObservableExceptional<T3> t3, IObservableExceptional<T4> t4
			, Func<T1, T2, T3, T4, TU> fn)
		{
			return Observable.CombineLatest(
						t1.Observable,
						t2.Observable,
						t3.Observable,
						t4.Observable,
						 (v1, v2, v3, v4)=>Exceptional.Combine(v1, v2, v3, v4,fn)
			).ToObservableExceptional();

		}

    
		/// <Summary>
		/// If any of the inputs are in error then the output will contain an aggregate
		/// exception of the inputs. If the selector function throws then the output will
		/// contain that exception.
		/// </Summary>
		public static IObservableExceptional<TU> CombineLatest<T1, T2, T3, T4, T5, TU>
			(this IObservableExceptional<T1> t1, IObservableExceptional<T2> t2, IObservableExceptional<T3> t3, IObservableExceptional<T4> t4, IObservableExceptional<T5> t5
			, Func<T1, T2, T3, T4, T5, TU> fn)
		{
			return Observable.CombineLatest(
						t1.Observable,
						t2.Observable,
						t3.Observable,
						t4.Observable,
						t5.Observable,
						 (v1, v2, v3, v4, v5)=>Exceptional.Combine(v1, v2, v3, v4, v5,fn)
			).ToObservableExceptional();

		}

    
		/// <Summary>
		/// If any of the inputs are in error then the output will contain an aggregate
		/// exception of the inputs. If the selector function throws then the output will
		/// contain that exception.
		/// </Summary>
		public static IObservableExceptional<TU> CombineLatest<T1, T2, T3, T4, T5, T6, TU>
			(this IObservableExceptional<T1> t1, IObservableExceptional<T2> t2, IObservableExceptional<T3> t3, IObservableExceptional<T4> t4, IObservableExceptional<T5> t5, IObservableExceptional<T6> t6
			, Func<T1, T2, T3, T4, T5, T6, TU> fn)
		{
			return Observable.CombineLatest(
						t1.Observable,
						t2.Observable,
						t3.Observable,
						t4.Observable,
						t5.Observable,
						t6.Observable,
						 (v1, v2, v3, v4, v5, v6)=>Exceptional.Combine(v1, v2, v3, v4, v5, v6,fn)
			).ToObservableExceptional();

		}

    
		/// <Summary>
		/// If any of the inputs are in error then the output will contain an aggregate
		/// exception of the inputs. If the selector function throws then the output will
		/// contain that exception.
		/// </Summary>
		public static IObservableExceptional<TU> CombineLatest<T1, T2, T3, T4, T5, T6, T7, TU>
			(this IObservableExceptional<T1> t1, IObservableExceptional<T2> t2, IObservableExceptional<T3> t3, IObservableExceptional<T4> t4, IObservableExceptional<T5> t5, IObservableExceptional<T6> t6, IObservableExceptional<T7> t7
			, Func<T1, T2, T3, T4, T5, T6, T7, TU> fn)
		{
			return Observable.CombineLatest(
						t1.Observable,
						t2.Observable,
						t3.Observable,
						t4.Observable,
						t5.Observable,
						t6.Observable,
						t7.Observable,
						 (v1, v2, v3, v4, v5, v6, v7)=>Exceptional.Combine(v1, v2, v3, v4, v5, v6, v7,fn)
			).ToObservableExceptional();

		}

    
		/// <Summary>
		/// If any of the inputs are in error then the output will contain an aggregate
		/// exception of the inputs. If the selector function throws then the output will
		/// contain that exception.
		/// </Summary>
		public static IObservableExceptional<TU> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, TU>
			(this IObservableExceptional<T1> t1, IObservableExceptional<T2> t2, IObservableExceptional<T3> t3, IObservableExceptional<T4> t4, IObservableExceptional<T5> t5, IObservableExceptional<T6> t6, IObservableExceptional<T7> t7, IObservableExceptional<T8> t8
			, Func<T1, T2, T3, T4, T5, T6, T7, T8, TU> fn)
		{
			return Observable.CombineLatest(
						t1.Observable,
						t2.Observable,
						t3.Observable,
						t4.Observable,
						t5.Observable,
						t6.Observable,
						t7.Observable,
						t8.Observable,
						 (v1, v2, v3, v4, v5, v6, v7, v8)=>Exceptional.Combine(v1, v2, v3, v4, v5, v6, v7, v8,fn)
			).ToObservableExceptional();

		}

    
		/// <Summary>
		/// If any of the inputs are in error then the output will contain an aggregate
		/// exception of the inputs. If the selector function throws then the output will
		/// contain that exception.
		/// </Summary>
		public static IObservableExceptional<TU> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, TU>
			(this IObservableExceptional<T1> t1, IObservableExceptional<T2> t2, IObservableExceptional<T3> t3, IObservableExceptional<T4> t4, IObservableExceptional<T5> t5, IObservableExceptional<T6> t6, IObservableExceptional<T7> t7, IObservableExceptional<T8> t8, IObservableExceptional<T9> t9
			, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TU> fn)
		{
			return Observable.CombineLatest(
						t1.Observable,
						t2.Observable,
						t3.Observable,
						t4.Observable,
						t5.Observable,
						t6.Observable,
						t7.Observable,
						t8.Observable,
						t9.Observable,
						 (v1, v2, v3, v4, v5, v6, v7, v8, v9)=>Exceptional.Combine(v1, v2, v3, v4, v5, v6, v7, v8, v9,fn)
			).ToObservableExceptional();

		}

    
		/// <Summary>
		/// If any of the inputs are in error then the output will contain an aggregate
		/// exception of the inputs. If the selector function throws then the output will
		/// contain that exception.
		/// </Summary>
		public static IObservableExceptional<TU> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TU>
			(this IObservableExceptional<T1> t1, IObservableExceptional<T2> t2, IObservableExceptional<T3> t3, IObservableExceptional<T4> t4, IObservableExceptional<T5> t5, IObservableExceptional<T6> t6, IObservableExceptional<T7> t7, IObservableExceptional<T8> t8, IObservableExceptional<T9> t9, IObservableExceptional<T10> t10
			, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TU> fn)
		{
			return Observable.CombineLatest(
						t1.Observable,
						t2.Observable,
						t3.Observable,
						t4.Observable,
						t5.Observable,
						t6.Observable,
						t7.Observable,
						t8.Observable,
						t9.Observable,
						t10.Observable,
						 (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10)=>Exceptional.Combine(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10,fn)
			).ToObservableExceptional();

		}

    
		/// <Summary>
		/// If any of the inputs are in error then the output will contain an aggregate
		/// exception of the inputs. If the selector function throws then the output will
		/// contain that exception.
		/// </Summary>
		public static IObservableExceptional<TU> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TU>
			(this IObservableExceptional<T1> t1, IObservableExceptional<T2> t2, IObservableExceptional<T3> t3, IObservableExceptional<T4> t4, IObservableExceptional<T5> t5, IObservableExceptional<T6> t6, IObservableExceptional<T7> t7, IObservableExceptional<T8> t8, IObservableExceptional<T9> t9, IObservableExceptional<T10> t10, IObservableExceptional<T11> t11
			, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TU> fn)
		{
			return Observable.CombineLatest(
						t1.Observable,
						t2.Observable,
						t3.Observable,
						t4.Observable,
						t5.Observable,
						t6.Observable,
						t7.Observable,
						t8.Observable,
						t9.Observable,
						t10.Observable,
						t11.Observable,
						 (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11)=>Exceptional.Combine(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11,fn)
			).ToObservableExceptional();

		}

    
		/// <Summary>
		/// If any of the inputs are in error then the output will contain an aggregate
		/// exception of the inputs. If the selector function throws then the output will
		/// contain that exception.
		/// </Summary>
		public static IObservableExceptional<TU> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TU>
			(this IObservableExceptional<T1> t1, IObservableExceptional<T2> t2, IObservableExceptional<T3> t3, IObservableExceptional<T4> t4, IObservableExceptional<T5> t5, IObservableExceptional<T6> t6, IObservableExceptional<T7> t7, IObservableExceptional<T8> t8, IObservableExceptional<T9> t9, IObservableExceptional<T10> t10, IObservableExceptional<T11> t11, IObservableExceptional<T12> t12
			, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TU> fn)
		{
			return Observable.CombineLatest(
						t1.Observable,
						t2.Observable,
						t3.Observable,
						t4.Observable,
						t5.Observable,
						t6.Observable,
						t7.Observable,
						t8.Observable,
						t9.Observable,
						t10.Observable,
						t11.Observable,
						t12.Observable,
						 (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12)=>Exceptional.Combine(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12,fn)
			).ToObservableExceptional();

		}

    
		/// <Summary>
		/// If any of the inputs are in error then the output will contain an aggregate
		/// exception of the inputs. If the selector function throws then the output will
		/// contain that exception.
		/// </Summary>
		public static IObservableExceptional<TU> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TU>
			(this IObservableExceptional<T1> t1, IObservableExceptional<T2> t2, IObservableExceptional<T3> t3, IObservableExceptional<T4> t4, IObservableExceptional<T5> t5, IObservableExceptional<T6> t6, IObservableExceptional<T7> t7, IObservableExceptional<T8> t8, IObservableExceptional<T9> t9, IObservableExceptional<T10> t10, IObservableExceptional<T11> t11, IObservableExceptional<T12> t12, IObservableExceptional<T13> t13
			, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TU> fn)
		{
			return Observable.CombineLatest(
						t1.Observable,
						t2.Observable,
						t3.Observable,
						t4.Observable,
						t5.Observable,
						t6.Observable,
						t7.Observable,
						t8.Observable,
						t9.Observable,
						t10.Observable,
						t11.Observable,
						t12.Observable,
						t13.Observable,
						 (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13)=>Exceptional.Combine(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13,fn)
			).ToObservableExceptional();

		}

    
		/// <Summary>
		/// If any of the inputs are in error then the output will contain an aggregate
		/// exception of the inputs. If the selector function throws then the output will
		/// contain that exception.
		/// </Summary>
		public static IObservableExceptional<TU> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TU>
			(this IObservableExceptional<T1> t1, IObservableExceptional<T2> t2, IObservableExceptional<T3> t3, IObservableExceptional<T4> t4, IObservableExceptional<T5> t5, IObservableExceptional<T6> t6, IObservableExceptional<T7> t7, IObservableExceptional<T8> t8, IObservableExceptional<T9> t9, IObservableExceptional<T10> t10, IObservableExceptional<T11> t11, IObservableExceptional<T12> t12, IObservableExceptional<T13> t13, IObservableExceptional<T14> t14
			, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TU> fn)
		{
			return Observable.CombineLatest(
						t1.Observable,
						t2.Observable,
						t3.Observable,
						t4.Observable,
						t5.Observable,
						t6.Observable,
						t7.Observable,
						t8.Observable,
						t9.Observable,
						t10.Observable,
						t11.Observable,
						t12.Observable,
						t13.Observable,
						t14.Observable,
						 (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14)=>Exceptional.Combine(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14,fn)
			).ToObservableExceptional();

		}

    
		/// <Summary>
		/// If any of the inputs are in error then the output will contain an aggregate
		/// exception of the inputs. If the selector function throws then the output will
		/// contain that exception.
		/// </Summary>
		public static IObservableExceptional<TU> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TU>
			(this IObservableExceptional<T1> t1, IObservableExceptional<T2> t2, IObservableExceptional<T3> t3, IObservableExceptional<T4> t4, IObservableExceptional<T5> t5, IObservableExceptional<T6> t6, IObservableExceptional<T7> t7, IObservableExceptional<T8> t8, IObservableExceptional<T9> t9, IObservableExceptional<T10> t10, IObservableExceptional<T11> t11, IObservableExceptional<T12> t12, IObservableExceptional<T13> t13, IObservableExceptional<T14> t14, IObservableExceptional<T15> t15
			, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TU> fn)
		{
			return Observable.CombineLatest(
						t1.Observable,
						t2.Observable,
						t3.Observable,
						t4.Observable,
						t5.Observable,
						t6.Observable,
						t7.Observable,
						t8.Observable,
						t9.Observable,
						t10.Observable,
						t11.Observable,
						t12.Observable,
						t13.Observable,
						t14.Observable,
						t15.Observable,
						 (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15)=>Exceptional.Combine(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15,fn)
			).ToObservableExceptional();

		}

    
		/// <Summary>
		/// If any of the inputs are in error then the output will contain an aggregate
		/// exception of the inputs. If the selector function throws then the output will
		/// contain that exception.
		/// </Summary>
		public static IObservableExceptional<TU> CombineLatest<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TU>
			(this IObservableExceptional<T1> t1, IObservableExceptional<T2> t2, IObservableExceptional<T3> t3, IObservableExceptional<T4> t4, IObservableExceptional<T5> t5, IObservableExceptional<T6> t6, IObservableExceptional<T7> t7, IObservableExceptional<T8> t8, IObservableExceptional<T9> t9, IObservableExceptional<T10> t10, IObservableExceptional<T11> t11, IObservableExceptional<T12> t12, IObservableExceptional<T13> t13, IObservableExceptional<T14> t14, IObservableExceptional<T15> t15, IObservableExceptional<T16> t16
			, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TU> fn)
		{
			return Observable.CombineLatest(
						t1.Observable,
						t2.Observable,
						t3.Observable,
						t4.Observable,
						t5.Observable,
						t6.Observable,
						t7.Observable,
						t8.Observable,
						t9.Observable,
						t10.Observable,
						t11.Observable,
						t12.Observable,
						t13.Observable,
						t14.Observable,
						t15.Observable,
						t16.Observable,
						 (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16)=>Exceptional.Combine(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16,fn)
			).ToObservableExceptional();

		}

    

	}

}