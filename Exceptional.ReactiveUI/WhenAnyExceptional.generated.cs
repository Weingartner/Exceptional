
using System;
using System.Collections.Generic;
using Weingartner.Exceptional;
using System.Linq.Expressions;
using ReactiveUI;
using Weingartner.Exceptional.Reactive;

namespace Weingartner.Exceptional.Reactive {
    public  static partial class ReactiveUI {

    
	/// <Summary>
	/// Any exceptions in the selector are wrapped and do not terminate the sequence
	/// </Summary>
    public static IObservableExceptional<TU> WhenAnyExceptional<TSender, T1, TU>
        ( this TSender sender, Expression<Func<TSender,T1>> x1
        , Func<T1, TU> fn)
    {
		return sender.WhenAnyValue(x1, (t1) => Exceptional.Execute(()=>fn(t1))).ToObservableExceptional();
    }

    
	/// <Summary>
	/// Any exceptions in the selector are wrapped and do not terminate the sequence
	/// </Summary>
    public static IObservableExceptional<TU> WhenAnyExceptional<TSender, T1, T2, TU>
        ( this TSender sender, Expression<Func<TSender,T1>> x1, Expression<Func<TSender,T2>> x2
        , Func<T1, T2, TU> fn)
    {
		return sender.WhenAnyValue(x1, x2, (t1, t2) => Exceptional.Execute(()=>fn(t1, t2))).ToObservableExceptional();
    }

    
	/// <Summary>
	/// Any exceptions in the selector are wrapped and do not terminate the sequence
	/// </Summary>
    public static IObservableExceptional<TU> WhenAnyExceptional<TSender, T1, T2, T3, TU>
        ( this TSender sender, Expression<Func<TSender,T1>> x1, Expression<Func<TSender,T2>> x2, Expression<Func<TSender,T3>> x3
        , Func<T1, T2, T3, TU> fn)
    {
		return sender.WhenAnyValue(x1, x2, x3, (t1, t2, t3) => Exceptional.Execute(()=>fn(t1, t2, t3))).ToObservableExceptional();
    }

    
	/// <Summary>
	/// Any exceptions in the selector are wrapped and do not terminate the sequence
	/// </Summary>
    public static IObservableExceptional<TU> WhenAnyExceptional<TSender, T1, T2, T3, T4, TU>
        ( this TSender sender, Expression<Func<TSender,T1>> x1, Expression<Func<TSender,T2>> x2, Expression<Func<TSender,T3>> x3, Expression<Func<TSender,T4>> x4
        , Func<T1, T2, T3, T4, TU> fn)
    {
		return sender.WhenAnyValue(x1, x2, x3, x4, (t1, t2, t3, t4) => Exceptional.Execute(()=>fn(t1, t2, t3, t4))).ToObservableExceptional();
    }

    
	/// <Summary>
	/// Any exceptions in the selector are wrapped and do not terminate the sequence
	/// </Summary>
    public static IObservableExceptional<TU> WhenAnyExceptional<TSender, T1, T2, T3, T4, T5, TU>
        ( this TSender sender, Expression<Func<TSender,T1>> x1, Expression<Func<TSender,T2>> x2, Expression<Func<TSender,T3>> x3, Expression<Func<TSender,T4>> x4, Expression<Func<TSender,T5>> x5
        , Func<T1, T2, T3, T4, T5, TU> fn)
    {
		return sender.WhenAnyValue(x1, x2, x3, x4, x5, (t1, t2, t3, t4, t5) => Exceptional.Execute(()=>fn(t1, t2, t3, t4, t5))).ToObservableExceptional();
    }

    
	/// <Summary>
	/// Any exceptions in the selector are wrapped and do not terminate the sequence
	/// </Summary>
    public static IObservableExceptional<TU> WhenAnyExceptional<TSender, T1, T2, T3, T4, T5, T6, TU>
        ( this TSender sender, Expression<Func<TSender,T1>> x1, Expression<Func<TSender,T2>> x2, Expression<Func<TSender,T3>> x3, Expression<Func<TSender,T4>> x4, Expression<Func<TSender,T5>> x5, Expression<Func<TSender,T6>> x6
        , Func<T1, T2, T3, T4, T5, T6, TU> fn)
    {
		return sender.WhenAnyValue(x1, x2, x3, x4, x5, x6, (t1, t2, t3, t4, t5, t6) => Exceptional.Execute(()=>fn(t1, t2, t3, t4, t5, t6))).ToObservableExceptional();
    }

    
	/// <Summary>
	/// Any exceptions in the selector are wrapped and do not terminate the sequence
	/// </Summary>
    public static IObservableExceptional<TU> WhenAnyExceptional<TSender, T1, T2, T3, T4, T5, T6, T7, TU>
        ( this TSender sender, Expression<Func<TSender,T1>> x1, Expression<Func<TSender,T2>> x2, Expression<Func<TSender,T3>> x3, Expression<Func<TSender,T4>> x4, Expression<Func<TSender,T5>> x5, Expression<Func<TSender,T6>> x6, Expression<Func<TSender,T7>> x7
        , Func<T1, T2, T3, T4, T5, T6, T7, TU> fn)
    {
		return sender.WhenAnyValue(x1, x2, x3, x4, x5, x6, x7, (t1, t2, t3, t4, t5, t6, t7) => Exceptional.Execute(()=>fn(t1, t2, t3, t4, t5, t6, t7))).ToObservableExceptional();
    }

    
	/// <Summary>
	/// Any exceptions in the selector are wrapped and do not terminate the sequence
	/// </Summary>
    public static IObservableExceptional<TU> WhenAnyExceptional<TSender, T1, T2, T3, T4, T5, T6, T7, T8, TU>
        ( this TSender sender, Expression<Func<TSender,T1>> x1, Expression<Func<TSender,T2>> x2, Expression<Func<TSender,T3>> x3, Expression<Func<TSender,T4>> x4, Expression<Func<TSender,T5>> x5, Expression<Func<TSender,T6>> x6, Expression<Func<TSender,T7>> x7, Expression<Func<TSender,T8>> x8
        , Func<T1, T2, T3, T4, T5, T6, T7, T8, TU> fn)
    {
		return sender.WhenAnyValue(x1, x2, x3, x4, x5, x6, x7, x8, (t1, t2, t3, t4, t5, t6, t7, t8) => Exceptional.Execute(()=>fn(t1, t2, t3, t4, t5, t6, t7, t8))).ToObservableExceptional();
    }

    
	/// <Summary>
	/// Any exceptions in the selector are wrapped and do not terminate the sequence
	/// </Summary>
    public static IObservableExceptional<TU> WhenAnyExceptional<TSender, T1, T2, T3, T4, T5, T6, T7, T8, T9, TU>
        ( this TSender sender, Expression<Func<TSender,T1>> x1, Expression<Func<TSender,T2>> x2, Expression<Func<TSender,T3>> x3, Expression<Func<TSender,T4>> x4, Expression<Func<TSender,T5>> x5, Expression<Func<TSender,T6>> x6, Expression<Func<TSender,T7>> x7, Expression<Func<TSender,T8>> x8, Expression<Func<TSender,T9>> x9
        , Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TU> fn)
    {
		return sender.WhenAnyValue(x1, x2, x3, x4, x5, x6, x7, x8, x9, (t1, t2, t3, t4, t5, t6, t7, t8, t9) => Exceptional.Execute(()=>fn(t1, t2, t3, t4, t5, t6, t7, t8, t9))).ToObservableExceptional();
    }

    
	/// <Summary>
	/// Any exceptions in the selector are wrapped and do not terminate the sequence
	/// </Summary>
    public static IObservableExceptional<TU> WhenAnyExceptional<TSender, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TU>
        ( this TSender sender, Expression<Func<TSender,T1>> x1, Expression<Func<TSender,T2>> x2, Expression<Func<TSender,T3>> x3, Expression<Func<TSender,T4>> x4, Expression<Func<TSender,T5>> x5, Expression<Func<TSender,T6>> x6, Expression<Func<TSender,T7>> x7, Expression<Func<TSender,T8>> x8, Expression<Func<TSender,T9>> x9, Expression<Func<TSender,T10>> x10
        , Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TU> fn)
    {
		return sender.WhenAnyValue(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) => Exceptional.Execute(()=>fn(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10))).ToObservableExceptional();
    }

    
	/// <Summary>
	/// Any exceptions in the selector are wrapped and do not terminate the sequence
	/// </Summary>
    public static IObservableExceptional<TU> WhenAnyExceptional<TSender, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TU>
        ( this TSender sender, Expression<Func<TSender,T1>> x1, Expression<Func<TSender,T2>> x2, Expression<Func<TSender,T3>> x3, Expression<Func<TSender,T4>> x4, Expression<Func<TSender,T5>> x5, Expression<Func<TSender,T6>> x6, Expression<Func<TSender,T7>> x7, Expression<Func<TSender,T8>> x8, Expression<Func<TSender,T9>> x9, Expression<Func<TSender,T10>> x10, Expression<Func<TSender,T11>> x11
        , Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TU> fn)
    {
		return sender.WhenAnyValue(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) => Exceptional.Execute(()=>fn(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11))).ToObservableExceptional();
    }

    
	/// <Summary>
	/// Any exceptions in the selector are wrapped and do not terminate the sequence
	/// </Summary>
    public static IObservableExceptional<TU> WhenAnyExceptional<TSender, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TU>
        ( this TSender sender, Expression<Func<TSender,T1>> x1, Expression<Func<TSender,T2>> x2, Expression<Func<TSender,T3>> x3, Expression<Func<TSender,T4>> x4, Expression<Func<TSender,T5>> x5, Expression<Func<TSender,T6>> x6, Expression<Func<TSender,T7>> x7, Expression<Func<TSender,T8>> x8, Expression<Func<TSender,T9>> x9, Expression<Func<TSender,T10>> x10, Expression<Func<TSender,T11>> x11, Expression<Func<TSender,T12>> x12
        , Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TU> fn)
    {
		return sender.WhenAnyValue(x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => Exceptional.Execute(()=>fn(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12))).ToObservableExceptional();
    }

    

    }
}