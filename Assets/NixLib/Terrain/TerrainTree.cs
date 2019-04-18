using System;
using System.Reflection;
using UnityEngine;

static class TerrainTree{
    //static public void Plant(Terrain terrain, Vector3 pos, int index) {
    //    TreeInstance instance = new TreeInstance();
    //    instance.prototypeIndex = index;
    //    instance.color = Color.white;
    //    instance.lightmapColor = Color.white;
    //    instance.widthScale = 1;
    //    instance.heightScale = 1;

    //    var relPos = pos - terrain.GetPosition();
    //    var position = new Vector3(relPos.x / terrain.terrainData.heightmapWidth, 0,
    //        relPos.z / terrain.terrainData.heightmapHeight);

    //    instance.position = position;
    //    terrain.AddTreeInstance(instance);
    //}
    //#region Reflection
    ///// <summary>
    ///// 移除地形上的树，没有做多地图的处理
    ///// </summary>
    ///// <param name="terrain">目标地形</param>
    ///// <param name="center">中心点</param>
    ///// <param name="radius">半径</param>
    ///// <param name="index">树模板的索引</param>
    //public static void RemoveTree(Terrain terrain, Vector3 center, float radius, int index = 0) {
    //    center -= terrain.GetPosition();     // 转为相对位置
    //    Vector2 v2 = new Vector2(center.x, center.z);
    //    v2.x /= Terrain.activeTerrain.terrainData.size.x;
    //    v2.y /= Terrain.activeTerrain.terrainData.size.z;
    //    terrain.Invoke("RemoveTrees", v2, radius / Terrain.activeTerrain.terrainData.size.x, index);
    //}
    ///// <summary>
    ///// 通过反射和函数名调用非公有方法
    ///// </summary>
    ///// <param name="obj">目标对象</param>
    ///// <param name="methodName">函数名</param>
    ///// <param name="objs">参数数组</param>
    //public static void Invoke(this object obj, string methodName, params object[] objs) {
    //    BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
    //    Type type = obj.GetType();
    //    MethodInfo m = type.GetMethod(methodName, flags);
    //    m.Invoke(obj, objs);
    //}
    //#endregion
}
